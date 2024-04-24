using System;
using System.Collections.Generic;
using System.Linq;
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
            Task.Run(() => DownloadTask(progressIndicator, cancellationTokenSource.Token));
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
            cancellationTokenSource.Cancel();
            MessageBox.Show("Download task has been cancelled.");

            // 导航回到Page2
            NavigationService.Navigate(new Page2());
        }

        private void CancelButton复制__C__Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());
        }
    }
}

