using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
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
using System.Windows.Shapes;

namespace MSWebView4WPF.FormView
{
    /// <summary>
    /// WebView2OriginalWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WebView2OriginalWindow : Window
    {
        private WebView2? _webView;


        public WebView2OriginalWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// WebView2 控件异步初始化事件
        /// </summary>
        /// <returns>An object that represents the current operation.</returns>
        private async Task InitializeAsync()
        {
            _webView = new WebView2();

            // WebView2 环境变量相关配置
            string webViewFolder = System.IO.Path.GetFullPath("D:/data/WebView2Fixed");
            string userDataFolder = System.IO.Path.GetFullPath("D:/data/WebView2Fixed/UserData");

            //string webViewFolder = System.IO.Path.GetFullPath("runtimes/WebView2");
            //string userDataPath = System.IO.Path.GetFullPath("runtimes/WebView2/UserData");

            CoreWebView2EnvironmentOptions envOptions = new(additionalBrowserArguments: null,
                                                            language: "zh-CN",
                                                            targetCompatibleBrowserVersion: null,
                                                            allowSingleSignOnUsingOSPrimaryAccount: false);

            CoreWebView2Environment envRuntime = await CoreWebView2Environment.CreateAsync(browserExecutableFolder: webViewFolder,
                                                                                           userDataFolder: userDataFolder,
                                                                                           options: envOptions);


            // 创建 WebView2 控件的 CreationProperties 属性对象
            //CoreWebView2CreationProperties envProperties = new()
            //{
            //    BrowserExecutableFolder = webViewFolder,
            //    UserDataFolder = userDataFolder,
            //    Language = "zh-CN"
            //};

            // 设置 CreationProperties 属性
            //_webView.CreationProperties = envProperties;


            // 在控件未创建时执行初始化将会失败，或者一直处于等待状态，特别是在 WPF 开发时。
            _webView.Loaded += async (sender, e) =>
            {
                // 调用 EnsureCoreWebView2Async 方法初始化底层
                await _webView.EnsureCoreWebView2Async(envRuntime);

                // 设置 WebView2 起始加载页面 https://news.baidu.com
                _webView.Source = new Uri(@"about:blank");


                // 禁用所有访问网络浏览器特定功能的加速器按键
                _webView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;

                // 禁止显示默认的上下文菜单
                _webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

                // 是否渲染默认的Javascript对话框，如alert,confirm,prompt,beforeunload等对话框。
                _webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = true;

                // 禁止使用上下文菜单或键盘快捷键来打开DevTools窗口
                _webView.CoreWebView2.Settings.AreDevToolsEnabled = false;

                // 禁止 web 内容访问主机对象
                _webView.CoreWebView2.Settings.AreHostObjectsAllowed = false;

                // 是否禁用导航失败和渲染过程失败的内置错误页面，禁用将显示空白页。
                _webView.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = true;

                // 是否启用一般表格信息等内容信息的保存和自动填写
                _webView.CoreWebView2.Settings.IsGeneralAutofillEnabled = true;

                // 禁止自动保存密码信息
                _webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;

                // 禁止支持触摸输入的设备上使用捏动动作来缩放WebView2中的网页内容
                _webView.CoreWebView2.Settings.IsPinchZoomEnabled = false;

                // 是否允许运行 JavaScript 脚本，不影响ExecuteScriptAsync方法执行脚本。
                _webView.CoreWebView2.Settings.IsScriptEnabled = true;

                // 是否显示状态栏
                _webView.CoreWebView2.Settings.IsStatusBarEnabled = false;

                // 是否在支持触摸输入的设备上使用刷卡手势来浏览WebView2
                _webView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;

                // 是否允许从主机到WebView的顶级HTML文档的通信
                _webView.CoreWebView2.Settings.IsWebMessageEnabled = true;

                // 是否允许使用鼠标滚轮和键盘操作，来缩放WebView控件中的内容；不影响ZoomFactor属性。
                _webView.CoreWebView2.Settings.IsZoomControlEnabled = true;


                _webView.CoreWebView2.NewWindowRequested += (sender, e) =>
                {
                    e.Handled = true;
                    _webView.CoreWebView2.Navigate(e.Uri);
                };

                _webView.CoreWebView2.NavigationStarting += (sender, e) =>
                {
                    // do something.
                };

                _webView.CoreWebView2.FrameNavigationStarting += (sender, e) =>
                {
                    // do something.
                };

                _webView.CoreWebView2.WebMessageReceived += (sender, e) =>
                {
                    // do something.
                };

                _webView.CoreWebView2InitializationCompleted += (sender, e) =>
                {
                    // do something.
                };

                _webView.ZoomFactorChanged += (sender, e) =>
                {
                    // do something.
                };
            };

            MainContent.Child = _webView;
        }

        /// <summary>
        /// 内容渲染事件
        /// </summary>
        /// <param name="e">传递事件</param>
        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            try
            {
                await InitializeAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void GoBackExecuted(object target, ExecutedRoutedEventArgs e)
        {
            _webView?.CoreWebView2.GoBack();
        }

        private void GoForwardExecuted(object target, ExecutedRoutedEventArgs e)
        {
            _webView?.CoreWebView2.GoForward();
        }

        private void NavigateExecuted(object target, ExecutedRoutedEventArgs e)
        {
            if (_webView != null && _webView.CoreWebView2 != null)
            {
                _webView.CoreWebView2.Navigate(TxtAddress.Text.Trim());
            }
        }

        private void VirtualMappingExecuted(object target, ExecutedRoutedEventArgs e)
        {
            if (_webView != null && _webView.CoreWebView2 != null)
            {
                string hostName = TxtHostName.Text.Trim();
                string folderPath = TxtFolderPath.Text.Trim();
                _webView.CoreWebView2.SetVirtualHostNameToFolderMapping(hostName, folderPath, CoreWebView2HostResourceAccessKind.Allow);

                MessageBox.Show($"完成本地目录（{folderPath}）到虚拟主机（{hostName}）的映射。", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
