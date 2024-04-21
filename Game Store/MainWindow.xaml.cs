using Game_Store.UI;
using iNKORE.UI.WPF.Modern.Controls;
using iNKORE.UI.WPF.Modern.Media.Animation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = System.Windows.Controls.Page;

namespace Game_Store
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var suggestions = new List<string>
    {
        "游戏1",
        "游戏2",
        "游戏3",
        // 更多游戏...
    };

            AutoSuggestBox.ItemsSource = suggestions;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("UI/Page2.xaml", UriKind.Relative));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page1.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page4.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page5.xaml", UriKind.RelativeOrAbsolute));
        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PagesNavigation_Navigated(object sender, NavigationEventArgs e)
        {
            Page newPage = e.Content as Page;
            if (newPage != null)
            {
                newPage.Loaded += NewPage_Loaded;
            }
        }

        private void NewPage_Loaded(object sender, RoutedEventArgs e)
        {
            Page newPage = sender as Page;
            if (newPage != null)
            {
                newPage.Loaded -= NewPage_Loaded;

                TranslateTransform translateTransform = new TranslateTransform();
                newPage.RenderTransform = translateTransform;

                DoubleAnimation slideUpAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = -70,
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut, Power = 3 }
                };

                translateTransform.BeginAnimation(TranslateTransform.YProperty, slideUpAnimation);
            }
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
    }
}
