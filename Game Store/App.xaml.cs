using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game_Store
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static event EventHandler<BackgroundChangedEventArgs> BackgroundChanged;

        public static void RaiseBackgroundChanged(Color? color, string imagePath)
        {
            BackgroundChanged?.Invoke(null, new BackgroundChangedEventArgs(color, imagePath));
        }
    }
    public class BackgroundChangedEventArgs : EventArgs
    {
        public Color? Color { get; }
        public string ImagePath { get; }

        public BackgroundChangedEventArgs(Color? color, string imagePath)
        {
            Color = color;
            ImagePath = imagePath;
        }
    }

}
