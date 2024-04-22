using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Game_Store.UI.Page2;
using Path = System.IO.Path;

namespace Game_Store.UI
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        private string installPath;
        public class Game
        {
            public string Name { get; set; }
            public string Size { get; set; }
            public string DownloadLink { get; set; }
            public int Number { get; set; }
        }
        public Page2()
        {
            InitializeComponent();
            LoadGames();
        }
        private void LoadGames()
        {
            var games = new List<Game>
        {
            new Game { Number = 1, Name = "游戏1", Size = "1GB", DownloadLink = "https://download.berrygm.com/static4/20240417/Steamberry_42_2.exe" },
            new Game { Number = 2, Name = "游戏1", Size = "1GB", DownloadLink = "https://pic.616pic.com/ys_bnew_img/00/39/23/GCfdbdGW2K.jpg" },
            new Game { Number = 3, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 4, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 5, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 6, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 7, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 8, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 9, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 10, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 11, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 12, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 13, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 14, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 15, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 16, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 17, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 18, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 19, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 20, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 21, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 22, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 23, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 24, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
            new Game { Number = 25, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },

                                                                                                                                               
                                                                                                                               
            // 更多游戏...
        };

            GameListView.ItemsSource = games;
        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            // 处理删除操作
            MessageBox.Show("删除操作");
        }

        private async void MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (GameListView.SelectedItem is Game selectedGame)
            {
                // 弹出 SaveFileDialog 以便用户选择保存路径
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                saveFileDialog.FileName = Path.GetFileName(selectedGame.DownloadLink); // 建议的文件名

                if (saveFileDialog.ShowDialog() == true)
                {
                    // 创建 Page3 的实例
                    var progressPage = new Page3();

                    // 导航到 Page3
                    NavigationService.Navigate(progressPage);

                    // 开始下载并传递 Page3 实例
                    await DownloadFileAsync(selectedGame.DownloadLink, saveFileDialog.FileName, progressPage);
                }
            }
        }

        private async Task DownloadFileAsync(string downloadLink, string filePath, Page3 progressPage)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 发送GET请求
                    HttpResponseMessage response = await client.GetAsync(downloadLink, HttpCompletionOption.ResponseHeadersRead);

                    if (response.IsSuccessStatusCode)
                    {
                        // 确保文件路径中的目录存在
                        var directoryPath = Path.GetDirectoryName(filePath);
                        Directory.CreateDirectory(directoryPath);

                        // 获取响应内容的流
                        using (var contentStream = await response.Content.ReadAsStreamAsync())
                        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            // 读取内容至文件
                            var totalReadBytes = 0L;
                            var totalBytes = response.Content.Headers.ContentLength ?? 0;
                            var buffer = new byte[8192];
                            var isMoreToRead = true;

                            do
                            {
                                var readBytes = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                                if (readBytes == 0)
                                {
                                    isMoreToRead = false;
                                    continue;
                                }

                                await fileStream.WriteAsync(buffer, 0, readBytes);
                                totalReadBytes += readBytes;

                                // 更新下载进度
                                var progress = (totalReadBytes * 100.0) / totalBytes;
                                progressPage.Dispatcher.Invoke(() =>
                                {
                                    progressPage.UpdateDownloadProgress(progress);
                                });
                            }
                            while (isMoreToRead);

                            // 下载完成
                            MessageBox.Show("下载完成");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"下载失败：{response.ReasonPhrase}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"下载发生错误：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void NavigateToSpecificPage()
        {
            Dispatcher.Invoke(() =>
            {
                // 导航到指定的页面，例如 'Page1'
                NavigationService.Navigate(new Page2());
            });
        }
    }
}

