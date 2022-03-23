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
            MessageBox.Show(ex.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


    #region ��Ա����

    /// <summary>
    /// WebView2 �ؼ��첽��ʼ���¼�
    /// </summary>
    /// <returns>An object that represents the current operation.</returns>
    private async Task InitializeAsync()
    {
        _webView = new WebView2();
        _webView.Dock = DockStyle.Fill;

        // WebView2 ���������������
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

        // ���� WebView2 �ؼ��� CreationProperties ���Զ���
        CoreWebView2CreationProperties envProperties = new()
        {
            BrowserExecutableFolder = webViewFolder,
            UserDataFolder = userDataFolder,
            Language = "zh-CN"
        };

        // ���� CreationProperties ����
        _webView.CreationProperties = envProperties;

        // �ڿؼ�δ����ʱִ�г�ʼ������ʧ�ܣ�����һֱ���ڵȴ�״̬���ر����� WPF ����ʱ��
        _webView.HandleCreated += async (sender, e) =>
        {
            // ���� EnsureCoreWebView2Async ������ʼ���ײ�
            await _webView.EnsureCoreWebView2Async(envRuntime);

            // ���� WebView2 ��ʼ����ҳ��
            _webView.Source = new Uri(@"https://news.baidu.com");


            // ��ֹ web ���ݷ�����������
            _webView.CoreWebView2.Settings.AreHostObjectsAllowed = false;

            // ��ֹ web ���ݽ� web ��Ϣ����������Ӧ�ó���
            _webView.CoreWebView2.Settings.IsWebMessageEnabled = false;

            // ��ֹ web �������нű����磺����ʾ��̬ html ����ʱ��
            _webView.CoreWebView2.Settings.IsScriptEnabled = false;

            // ��ֹ��ʾ web ���ݻ�Ի���
            _webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;


            // �¼�ע��

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

            MessageBox.Show($"��ɱ���Ŀ¼��{folderPath}��������������{hostName}����ӳ�䡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
