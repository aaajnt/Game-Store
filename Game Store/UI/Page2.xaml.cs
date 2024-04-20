using MahApps.Metro.Controls.Dialogs;
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
            new Game { Number = 2, Name = "游戏1", Size = "1GB", DownloadLink = "http://example.com/game1" },
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
                // 显示下载链接
                MessageBox.Show("下载链接: " + selectedGame.DownloadLink);

                // 显示输入框让用户输入安装路径
                installPath = ShowInstallPathDialog();
                if (!string.IsNullOrEmpty(installPath))
                {
                    // 调用下载方法
                    await DownloadFileAsync(selectedGame.DownloadLink, installPath);
                }
            }
        }

        private string ShowInstallPathDialog()
        {
            // 创建一个简单的输入框对话框
            string installPath = string.Empty;
            var inputDialog = new InputDialog("请输入安装路径", "安装路径", "确定", "取消");
            if (inputDialog.ShowDialog() == true)
            {
                installPath = inputDialog.InputText;
            }
            return installPath;
        }

        private async Task DownloadFileAsync(string downloadLink, string installPath)
        {
            using (HttpClient client = new HttpClient())
            {
                // 发送GET请求
                HttpResponseMessage response = await client.GetAsync(downloadLink);

                // 检查响应状态
                if (response.IsSuccessStatusCode)
                {
                    // 获取响应内容的流
                    Stream contentStream = await response.Content.ReadAsStreamAsync();

                    // 创建文件路径
                    string fileName = Path.GetFileName(downloadLink);
                    string filePath = Path.Combine(installPath, fileName);

                    // 将流写入文件
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await contentStream.CopyToAsync(fileStream);
                    }

                    // 下载完成
                    MessageBox.Show("下载完成");
                }
                else
                {
                    // 处理错误
                    MessageBox.Show("下载失败");
                }
            }
        }
    }

    public class Game
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string DownloadLink { get; set; }
    }

    public class InputDialog : Window
    {
        private TextBox inputTextBox;

        public InputDialog(string title, string label, string okText, string cancelText)
        {
            Title = title;
            Width = 300;
            Height = 150;
            ResizeMode = ResizeMode.NoResize;
            Button okButton = new Button { Content = okText, Width = 75, Margin = new Thickness(0, 10, 0, 0) };
            Button cancelButton = new Button { Content = cancelText, Width = 75, Margin = new Thickness(0, 10, 0, 0) };

            inputTextBox = new TextBox { Width = 200, Margin = new Thickness(0, 0, 0, 10) };

            okButton.Click += (s, e) => DialogResult = true;
            cancelButton.Click += (s, e) => DialogResult = false;

            Content = new StackPanel
            {
                Children =
                {
                    new Label { Content = label, Margin = new Thickness(0, 10, 0, 0) },
                    inputTextBox,
                    new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Children = { okButton, cancelButton }
                    }
                }
            };
        }

        public string InputText => inputTextBox.Text;
    }
}

