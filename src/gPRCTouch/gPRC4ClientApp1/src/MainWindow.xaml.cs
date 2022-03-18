using gPRC4ClientApp1.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using Google.Protobuf;
using Grpc.Net.Client;

namespace gPRC4ClientApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private static readonly string[] emptyArray = Array.Empty<string>();
        //private string[] _currentSelectedFiles = emptyArray;
        //private int _chunkSize = 1024 * 32; // 32 KB
        //private SightX.Uploader.UploaderClient? _client;


        public MainWindow()
        {
            InitializeComponent();
        }


        protected override void OnInitialized(EventArgs e)
        {
            //_currentSelectedFiles = emptyArray;
            DataContext = new FieldsViewModel()
            {
                //Name = @"http://localhost:5000"
                Name = @"https://localhost:5001"
            };

            base.OnInitialized(e);
        }


        private void FileBrowserExecuted(object target, ExecutedRoutedEventArgs e)
        {
            //_currentSelectedFiles = emptyArray;

            OpenFileDialog dialog = new();
            dialog.Title = "请选择需要上传的图片";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg";
            dialog.Multiselect = true;

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                TxtUploadFiles.Text = string.Join(',', dialog.FileNames);
                //_currentSelectedFiles = dialog.FileNames;
            }
        }

        private void UploadExecuted(object target, ExecutedRoutedEventArgs e)
        {
            //_currentSelectedFiles = emptyArray;
        }

        private void ServerAddressExecuted(object target, ExecutedRoutedEventArgs e)
        {
            using var channel = GrpcChannel.ForAddress(TxtServerAddress.Text.Trim());
            //var client = new SightX2gRPC.Uploader.UploaderClient(channel);
        }
    }
}
