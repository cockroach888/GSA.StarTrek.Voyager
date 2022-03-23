using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;

namespace MSWebView4WinForm;

public partial class MainForm : Form
{
    private WebView2? _webView;


    public MainForm()
    {
        InitializeComponent();

        _ = InitializeAsync();
    }


    protected override async void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        try
        {
            await InitializeAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_webView != null)
        {
            _webView.Size = ClientSize - new Size(_webView.Location);
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);

        _webView?.Dispose();
    }


    #region 成员方法

    /// <summary>
    /// WebView2 控件异步初始化事件
    /// </summary>
    /// <returns>An object that represents the current operation.</returns>
    private async Task InitializeAsync()
    {
        _webView = new WebView2();
        _webView.Dock = DockStyle.Fill;

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
        CoreWebView2CreationProperties envProperties = new()
        {
            BrowserExecutableFolder = webViewFolder,
            UserDataFolder = userDataFolder,
            Language = "zh-CN"
        };

        // 设置 CreationProperties 属性
        _webView.CreationProperties = envProperties;

        // 在控件未创建时执行初始化将会失败，或者一直处于等待状态，特别是在 WPF 开发时。
        _webView.HandleCreated += async (sender, e) =>
        {
            // 调用 EnsureCoreWebView2Async 方法初始化底层
            await _webView.EnsureCoreWebView2Async(envRuntime);

            // 设置 WebView2 起始加载页面
            _webView.Source = new Uri(@"https://news.baidu.com");


            // 禁止 web 内容访问主机对象
            _webView.CoreWebView2.Settings.AreHostObjectsAllowed = false;

            // 禁止 web 内容将 web 消息发布到本机应用程序
            _webView.CoreWebView2.Settings.IsWebMessageEnabled = false;

            // 禁止 web 内容运行脚本（如：当显示静态 html 内容时）
            _webView.CoreWebView2.Settings.IsScriptEnabled = false;

            // 禁止显示 web 内容或对话框
            _webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;


            // 事件注册

            _webView.CoreWebView2.NewWindowRequested += (sender, e) =>
            {
                //_webView.CoreWebView2.Navigate(e.Uri);
                //e.Handled = true;
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

        PalMain.Controls.Add(_webView);
    }

    #endregion


    private void BtnMapping_Click(object sender, EventArgs e)
    {
        if (_webView != null && _webView.CoreWebView2 != null)
        {
            string hostName = TxtHostName.Text.Trim();
            string folderPath = TxtFolderPath.Text.Trim();
            _webView.CoreWebView2.SetVirtualHostNameToFolderMapping(hostName, folderPath, CoreWebView2HostResourceAccessKind.Allow);

            MessageBox.Show($"完成本地目录（{folderPath}）到虚拟主机（{hostName}）的映射。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BtnDevTool_Click(object sender, EventArgs e)
    {
        if (_webView != null && _webView.CoreWebView2 != null)
        {
            _webView.CoreWebView2.OpenDevToolsWindow();
        }
    }

    private void BtnNavigate_Click(object sender, EventArgs e)
    {
        if (_webView != null && _webView.CoreWebView2 != null)
        {
            _webView.CoreWebView2.Navigate(TxtNavigate.Text.Trim());
        }
    }
}
