using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public MainWindow()
        {
            InitializeComponent();
        }


        private void WebView2OriginalExecuted(object target, ExecutedRoutedEventArgs e)
        {
            Window window = new FormView.WebView2OriginalWindow()
            {
                ShowActivated = true,
                ShowInTaskbar = false
            };
            window.Show();
        }

        private void FileBrowserExecuted(object target, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Title = "请选择需要上传的图片";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.tif";
            dialog.Multiselect = false;

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                TxtLoadingFiles.Text = string.Join(',', dialog.FileNames);
            }
        }

        private void LoadImageExecuted(object target, ExecutedRoutedEventArgs e)
        {
            string imageFilePath = TxtLoadingFiles.Text.Trim();

            if (!File.Exists(imageFilePath))
            {
                MessageBox.Show("请先选择需要加载的图片文件。");
                return;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageFilePath, UriKind.Absolute);
            bitmap.EndInit();

            ImgOriginMode.Source = bitmap;

            stopwatch.Stop();

            LblMessage.Content = $"图片加载耗时：{stopwatch.ElapsedMilliseconds}ms。";
        }
    }
}
