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
using System.Windows.Threading;

namespace MSWebView4WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly Dispatcher _mainDispatcher = Dispatcher.CurrentDispatcher;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadImageExecuted(object target, ExecutedRoutedEventArgs e)
        {
            string imageFilePath = @"E:\data\TestImage\HYP_LR.tif";

            Stopwatch stopwatch = Stopwatch.StartNew();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageFilePath);
            bitmap.EndInit();

            ImgOriginMode.Source = bitmap;

            //await _mainDispatcher.BeginInvoke(DispatcherPriority.Background, new Action<BitmapImage>((image) =>
            //{
            //    ImgOriginMode.Source = image;
            //}), bitmap);

            stopwatch.Stop();

            LblMessage.Content = $"图片加载耗时：{stopwatch.ElapsedMilliseconds}ms。";
        }
    }
}
