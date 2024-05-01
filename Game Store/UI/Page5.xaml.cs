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
using Game_Store.kind;
using System.Collections.ObjectModel;

namespace Game_Store.UI
{
    /// <summary>
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Page5 : Page
    {
        private SharedViewModel viewModel = new SharedViewModel();
        private SharedViewModel sharedViewModel = ((App)Application.Current).SharedViewModel;
        public static bool IsCheckBoxChecked { get; set; }
        public Page5()
        {
            InitializeComponent();
            this.Loaded += Page_Loaded;
            this.DataContext = sharedViewModel;
            this.DataContext = App.SharedVM;
            sharedViewModel = new SharedViewModel();
            this.DataContext = sharedViewModel;
            this.DataContext = new YourViewModel();
            this.Loaded += Page_Loaded;
            sharedViewModel = ((App)Application.Current).SharedViewModel;
            this.DataContext = sharedViewModel; // 只设置一次
            this.DataContext = viewModel;
            this.DataContext = ((App)Application.Current).SharedViewModel;

            // 确保在这里访问ComboBox之前，它已经被加载
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
            string savedSelection = Properties.Settings.Default.ComboBoxSelection;
            ComboBox comboBox = sender as ComboBox;
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
            if (comboBox == null) return;

            if (selectedItem == null) return;
            if (comboBox?.SelectedItem is ComboBoxItem)
            {
                Properties.Settings.Default.ThemeSelection = selectedItem.Content.ToString();
                Properties.Settings.Default.Save();
                // 如果你有ApplyTheme方法，就取消注释下面的代码行
                // ApplyTheme(selectedItem.Content.ToString());
            }

            _ = Properties.Settings.Default.DefaultComboBoxItem;
            foreach (ComboBoxItem item in backgroundTypeComboBox.Items)
            {
                if (item.Content.ToString() == savedSelection)
                {
                    backgroundTypeComboBox.SelectedItem = item;
                    break;
                }
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = sharedViewModel; // 设置DataContext
            RestoreComboBoxSelection();
            AddCustomImageComboBoxItem();
            SaveComboBoxSelection();


        }
        private void RestoreComboBoxSelection()
        {
            string savedSelection = Properties.Settings.Default.ThemeSelection;
            if (!string.IsNullOrEmpty(savedSelection))
            {
                foreach (ComboBoxItem item in backgroundTypeComboBox.Items)
                {
                    if (item.Content.ToString() == savedSelection)
                    {
                        backgroundTypeComboBox.SelectedItem = item;
                        // 如果你有ApplyTheme方法，就取消注释下面的代码行
                        // ApplyTheme(item.Content.ToString());
                        break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(savedSelection))
            {
                foreach (ComboBoxItem item in backgroundTypeComboBox.Items)
                {
                    if (item.Content.ToString() == savedSelection)
                    {
                        backgroundTypeComboBox.SelectedItem = item;
                        return; // 成功恢复选择，无需进一步处理
                    }
                }
                // 如果没有找到保存的项，您可能需要处理这种情况
                // 例如，可以选择一个默认项，或者显示一个错误消息
            }
            if (!string.IsNullOrEmpty(savedSelection) && backgroundTypeComboBox.Items != null)
            {
                foreach (ComboBoxItem item in backgroundTypeComboBox.Items)
                {
                    if (item != null && item.Content != null && item.Content.ToString() == savedSelection)
                    {
                        backgroundTypeComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            // 确保ComboBox已经加载了Items
            string lastSelection = Properties.Settings.Default.LastSelectedComboBoxItem;
            if (!string.IsNullOrEmpty(lastSelection))
            {
                foreach (ComboBoxItem item in backgroundTypeComboBox.Items)
                {
                    if (item.Content.ToString() == lastSelection)
                    {
                        backgroundTypeComboBox.SelectedItem = item;
                        return; // 找到匹配项并设置为选中状态，退出方法
                    }
                }
            }
        }
        private void SaveSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (backgroundTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Properties.Settings.Default.DefaultComboBoxItem = selectedItem.Content.ToString();
                Properties.Settings.Default.Save();
                MessageBox.Show("Selection saved successfully.");
            }
        }

        private void LoadComboBoxSelection()
        {
            string lastSelection = Properties.Settings.Default.LastSelectedComboBoxItem;
            foreach (ComboBoxItem item in backgroundTypeComboBox.Items)
            {
                if ((string)item.Content == lastSelection)
                {
                    backgroundTypeComboBox.SelectedItem = item;
                    break;
                }
            }
        }
        private void AddCustomImageComboBoxItem()
        {
            var customImageItem = backgroundTypeComboBox.Items
                .OfType<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == "Custom Image...");

            if (customImageItem == null)
            {
                backgroundTypeComboBox.Items.Add(new ComboBoxItem { Content = "Custom Image..." });
            }
        }
        public ObservableCollection<ComboBoxItem> ComboBoxItems { get; set; }
        public ComboBoxItem SelectedComboBoxItem { get; set; }

        // 加载数据并设置默认项
        private void LoadDataAndSetDefault()
        {
            // 假设LoadComboBoxItems()方法加载了数据到ComboBoxItems集合中
            LoadComboBoxItems();

            // 从设置中读取保存的项
            string savedSelection = Properties.Settings.Default.DefaultComboBoxItem;

            // 确保数据已加载
            if (ComboBoxItems.Any())
            {
                // 查找并设置默认项
                var defaultItem = ComboBoxItems.FirstOrDefault(item => item.Content.ToString() == savedSelection);
                if (defaultItem != null)
                {
                    SelectedComboBoxItem = defaultItem;
                }
                else
                {
                    // 如果没有找到保存的项，可以设置一个默认项或者不做任何操作
                    SelectedComboBoxItem = ComboBoxItems.First(); // 例如设置第一个项为默认
                }
            }
        }

        private void LoadComboBoxItems()
        {
            throw new NotImplementedException();
        }
        private void SaveComboBoxSelection()
        {
            if (backgroundTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Properties.Settings.Default.DefaultComboBoxItem = selectedItem.Content.ToString();
                Properties.Settings.Default.Save();
            }
        }
    }

    internal class YourViewModel
    {
        public YourViewModel()
        {
        }
    }
}
