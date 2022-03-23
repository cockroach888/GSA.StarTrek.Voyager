using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (Window item in OwnedWindows)
            {
                item.Close();
            }

            base.OnClosing(e);
        }



        private void WebView2OriginalExecuted(object target, ExecutedRoutedEventArgs e)
        {
            Window window = new FormView.WebView2OriginalWindow()
            {
                ShowActivated = true,
                ShowInTaskbar = false
            };
            window.Owner = this;
            window.Show();
        }

        private void WebView2FourImageExecuted(object target, ExecutedRoutedEventArgs e)
        {
            Window window = new FormView.WebView2FourImageWindow()
            {
                ShowActivated = true,
                ShowInTaskbar = false
            };
            window.Owner = this;
            window.Show();
        }

        private void FileBrowserExecuted(object target, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Title = "请选择需要加载的图片";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.tif;*.bmp";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                //TxtLoadingFiles.Text = string.Join(',', dialog.FileNames);
                TxtLoadingFiles.Text = dialog.FileName;
                LoadImage(dialog.FileName);
            }

            LstFileListView.Items.Clear();
        }

        private void FolderBrowserExecuted(object target, ExecutedRoutedEventArgs e)
        {
            string filePath = TxtLoadingFiles.Text.Trim();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("请先选择需要浏览的图片目录。");
                return;
            }

            LstFileListView.Items.Clear();
            string? folderPath = System.IO.Path.GetDirectoryName(filePath);
            if (folderPath != null)
            {
                IEnumerable<string> files = Directory.EnumerateFiles(folderPath);

                foreach (string file in files)
                {
                    if (file.EndsWith("html"))
                    {
                        continue;
                    }

                    ListViewItem item = new();
                    item.Content = System.IO.Path.GetFileName(file);
                    item.Tag = file;
                    item.Selected += (s, e) =>
                    {
                        LoadImage($"{((ListViewItem)e.Source).Tag}");
                    };

                    LstFileListView.Items.Add(item);
                }

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

            LoadImage(imageFilePath);
        }

        private void LoadImage(string imageFilePath)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            BitmapImage bitmap = new();
            bitmap.BeginInit();
            //bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(imageFilePath, UriKind.Absolute);
            bitmap.EndInit();

            ImgOriginMode.Source = bitmap;

            stopwatch.Stop();

            LblMessage.Content = $"图片加载耗时：{stopwatch.ElapsedMilliseconds}ms。";
        }
    }
}
