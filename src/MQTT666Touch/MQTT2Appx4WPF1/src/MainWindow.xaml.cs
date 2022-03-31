﻿using MahApps.Metro.Controls;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MQTT2Appx4WPF1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private WebView2? _webView;
        private IMqttClient _mqttClient;


        public MainWindow()
        {
            InitializeComponent();
        }

        #region WebView2 控件异步初始化事件

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

                // 设置 WebView2 起始加载页面
                _webView.Source = new Uri(@"https://news.baidu.com");



                // 禁用所有访问网络浏览器特定功能的加速器按键，允许则设置true。
                _webView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;

                // 禁止显示默认的上下文菜单，允许则设置true。当想使用自定义菜单时，需要设置为true并添加自定义菜单项。
                _webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

                // 是否渲染默认的Javascript对话框，如alert,confirm,prompt,beforeunload等对话框，允许则设置true。
                _webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = true;

                // 禁止使用上下文菜单或键盘快捷键来打开DevTools窗口，允许则设置true。
                _webView.CoreWebView2.Settings.AreDevToolsEnabled = false;

                // 禁止 web 内容访问主机对象，允许则设置true。如果注册了C#对象为JS函数则必须为true才行。
                _webView.CoreWebView2.Settings.AreHostObjectsAllowed = true;

                // 是否禁用导航失败和渲染过程失败的内置错误页面，禁用将显示空白页，允许则设置true。
                _webView.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = true;

                // 是否启用一般表格信息等内容信息的保存和自动填写，允许则设置true。
                _webView.CoreWebView2.Settings.IsGeneralAutofillEnabled = true;

                // 禁止自动保存密码信息，允许则设置true。
                _webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;

                // 禁止支持触摸输入的设备上使用捏动动作来缩放WebView2中的网页内容，允许则设置true。
                _webView.CoreWebView2.Settings.IsPinchZoomEnabled = false;

                // 是否允许运行 JavaScript 脚本，不影响ExecuteScriptAsync方法执行脚本，允许则设置true。
                _webView.CoreWebView2.Settings.IsScriptEnabled = true;

                // 是否显示状态栏，允许则设置true。
                _webView.CoreWebView2.Settings.IsStatusBarEnabled = false;

                // 是否在支持触摸输入的设备上使用刷卡手势来浏览WebView2，允许则设置true。
                _webView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;

                // 是否允许从主机到WebView的顶级HTML文档的通信，允许则设置true。
                _webView.CoreWebView2.Settings.IsWebMessageEnabled = true;

                // 是否允许使用鼠标滚轮和键盘操作，来缩放WebView控件中的内容；不影响ZoomFactor属性，允许则设置true。
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

        #endregion

        /// <summary>
        /// 窗体初始化响应事件
        /// </summary>
        /// <param name="e">传递事件</param>
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            try
            {
                await InitializeAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 窗体内容渲染响应事件
        /// </summary>
        /// <param name="e">传递事件</param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);



            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();


            using (_mqttClient = mqttFactory.CreateMqttClient())
            {
                // Use builder classes where possible in this project.
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();

                // This will throw an exception if the server is not available.
                // The result from this message returns additional data which was sent 
                // from the server. Please refer to the MQTT protocol specification for details.
                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                Console.WriteLine("The MQTT client is connected.");

                response.DumpToConsole();

                // Send a clean disconnect to the server by calling _DisconnectAsync_. Without this the TCP connection
                // gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
                var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

                await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
            }
        }

        /// <summary>
        /// 窗体关闭响应事件
        /// </summary>
        /// <param name="e">传递事件</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            _webView?.Dispose();

            base.OnClosing(e);
        }

        /// <summary>
        /// 窗体键盘按键弹起响应事件
        /// </summary>
        /// <param name="e">传递事件</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Key.F9)
            {
                _webView?.CoreWebView2.OpenDevToolsWindow();
            }
        }
    }
}
