using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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


namespace Game_Store.UI
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public Page3()
        {
            InitializeComponent();
            StartDownloadTask();
        }

        private void StartDownloadTask()
        {

            var progressIndicator = new Progress<double>(progress => UpdateDownloadProgress(progress));
            Task.Run(() => {
                try
                {
                    DownloadTask(progressIndicator, cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    // 捕获取消异常，执行取消后的逻辑，例如更新UI通知用户
                    Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Download was cancelled.");
                        // 可以在这里更新进度条或状态文本
                        DownloadProgressBar.Value = 0;
                        ProgressTextBlock.Text = "Download cancelled.";
                    });
                }
            });
        }

        private void DownloadTask(IProgress<double> progress, CancellationToken cancellationToken)
        {
            for (int i = 0; i <= 100; i++)
            {
                Task.Delay(100).Wait();

                cancellationToken.ThrowIfCancellationRequested();

                progress.Report(i);
            }
        }

        public void UpdateDownloadProgress(double progress)
        {
            Dispatcher.Invoke(() =>
            {
                DownloadProgressBar.Value = progress;
                ProgressTextBlock.Text = $"{progress}%";
            });
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // 请求取消操作
            cancellationTokenSource.Cancel();

            // 显示消息框通知用户下载已经被取消
            MessageBoxResult result = MessageBox.Show("Download task has been cancelled.", "Download Cancelled", MessageBoxButton.OK);

            // 检查消息框的结果
            if (result == MessageBoxResult.OK)
            {
                // 清理取消令牌源并为未来的下载任务准备一个新的实例
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new CancellationTokenSource();

                // 确保下载任务不会再次启动
                // 如果有必要，清理或禁用与下载相关的UI组件
                // ...

                // 导航回到Page2
                NavigationService.Navigate(new Page2());
            }
        }

        private void CancelButton复制__C__Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());
        }
    }
}

