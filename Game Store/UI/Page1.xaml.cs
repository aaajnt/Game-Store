using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Markdig;

namespace Game_Store.UI
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        private static readonly string giteeUrl = "https://gitee.com/musdds/Game-Store/raw/main/README.md";
        private static readonly string githubUrl = "https://raw.githubusercontent.com/aaajnt/Game-Store/main/README.md";
        public Page1()
        {
            InitializeComponent();
            LoadFileContentAsync();
        }

        private async void LoadFileContentAsync()
        {
            try
            {
                string fileContent = await GetFileContentAsync(giteeUrl);
                DisplayMarkdown(fileContent);
            }
            catch (TaskCanceledException)
            {
                // 如果Gitee的请求在5秒内没有响应，切换到Github
                try
                {
                    string fileContent = await GetFileContentAsync(githubUrl);
                    DisplayMarkdown(fileContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"无法加载Github文件内容: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法加载Gitee文件内容: {ex.Message}");
            }
        }

        private void DisplayMarkdown(string markdown)
        {
            // 使用Markdig转换Markdown到FlowDocument
            FlowDocument document = Markdig.Wpf.Markdown.ToFlowDocument(markdown);
            // 显示FlowDocument在FlowDocumentScrollViewer控件中
            markdownViewer.Document = document;
        }

        private static async Task<string> GetFileContentAsync(string url)
        {
            using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) })
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // 如果不成功，则抛出异常
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
        }

    }
}
