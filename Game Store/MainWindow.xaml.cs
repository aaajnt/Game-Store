using Game_Store.UI;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            PagesNavigation.Navigate(new System.Uri("UI/Page1.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page1.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page2.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page3.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page3.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("UI/Page3.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}
