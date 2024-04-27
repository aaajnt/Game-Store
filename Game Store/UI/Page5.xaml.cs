using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using SharpCompress.Archives;
using SharpCompress.Common;
using Path = System.IO.Path;
using ZipFile = System.IO.Compression.ZipFile;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using System.Windows.Media.Animation;
using Style = System.Windows.Style;
using Button = System.Windows.Controls.Button;

namespace Game_Store.UI
{
    /// <summary>
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Page5 : Page
    {
        public static bool IsCheckBoxChecked { get; set; }
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
        private void CheckAndUnzip(string filePath)
        {
            if (autoUnzipCheckBox.IsChecked == true)
            {
                try
                {
                    if (filePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    {
                        ZipFile.ExtractToDirectory(filePath, Path.GetDirectoryName(filePath));
                        MessageBox.Show($"文件 {Path.GetFileName(filePath)} 已成功解压。");
                    }
                    else if (filePath.EndsWith(".7z", StringComparison.OrdinalIgnoreCase) || filePath.EndsWith(".rar", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var archive = ArchiveFactory.Open(filePath))
                        {
                            foreach (var entry in archive.Entries)
                            {
                                if (!entry.IsDirectory)
                                {
                                    entry.WriteToDirectory(Path.GetDirectoryName(filePath), new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                                }
                            }
                        }
                        MessageBox.Show($"文件 {Path.GetFileName(filePath)} 已成功解压。");
                    }
                    else
                    {
                        // 非压缩文件格式，跳过解压。
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"解压缩错误: {ex.Message}");
                }
            }
            else
            {
                // 自动解压功能未开启，不执行解压操作
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsCheckBoxChecked = true;
            Properties.Settings.Default.Save();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsCheckBoxChecked = false;
            Properties.Settings.Default.Save();
        }
        private void BackgroundTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            switch (selectedItem.Content.ToString())
            {
                case "Restore Default Style":
                    RestoreDefaultStyle();
                    break;
                case "Red Background":
                    App.RaiseBackgroundChanged(Colors.Red, null);
                    break;
                case "Blue Background":
                    App.RaiseBackgroundChanged(Colors.Blue, null);
                    break;
                case "Green Background":
                    App.RaiseBackgroundChanged(Colors.Green, null);
                    break;
                case "Custom Image...":
                    UploadAndSetImageBackground();
                    break;
                case "Follow System Theme":
                    CheckSystemThemeAndRaiseEvent();
                    break;
            }
        }

        private void CheckSystemThemeAndRaiseEvent()
        {
            var isLightTheme = GetIsLightTheme();
            var color = isLightTheme ? Colors.White : Colors.Black;
            App.RaiseBackgroundChanged(color, null);
        }

        private bool GetIsLightTheme()
        {
            const string RegistryKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string RegistryValueName = "AppsUseLightTheme";

            object registryValueObject = Registry.GetValue(RegistryKeyPath, RegistryValueName, null);
            if (registryValueObject != null)
            {
                int registryValue = (int)registryValueObject;
                return registryValue > 0;
            }
            return true; // Default to light theme if unable to read the registry
        }
        private void UploadAndSetImageBackground()
        {

            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                App.RaiseBackgroundChanged(null, openFileDialog.FileName);
            }
        }
        private void RestoreDefaultStyle()
        {
            // 引发背景变更事件，传递透明作为背景颜色
            App.RaiseBackgroundChanged(Colors.Transparent, null);
        }


    }
}
