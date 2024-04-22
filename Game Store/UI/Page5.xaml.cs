using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace Game_Store.UI
{
    /// <summary>
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://github.com/aaajnt/Game-Store";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.youtube.com/channel/UCN8e_V85cThDPYb98pM2HhA";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void btn7复制__C__Click(object sender, RoutedEventArgs e)
        {
            string url = "https://afdian.net/a/anmingyun";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

    }

}
