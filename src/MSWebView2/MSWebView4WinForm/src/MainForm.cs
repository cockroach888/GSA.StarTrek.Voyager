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
        string webViewFolder = Path.GetFullPath("D:/data/WebView2Fixed");
        string userDataFolder = Path.GetFullPath("D:/data/WebView2Fixed/UserData");

        //string webViewFolder = Path.GetFullPath("runtimes/WebView2");
        //string userDataPath = Path.GetFullPath("runtimes/WebView2/UserData");

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



            // �������з�������������ض����ܵļ���������������������true��
            _webView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;

            // ��ֹ��ʾĬ�ϵ������Ĳ˵�������������true������ʹ���Զ���˵�ʱ����Ҫ����Ϊtrue������Զ���˵��
            _webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

            // �Ƿ���ȾĬ�ϵ�Javascript�Ի�����alert,confirm,prompt,beforeunload�ȶԻ�������������true��
            _webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = true;

            // ��ֹʹ�������Ĳ˵�����̿�ݼ�����DevTools���ڣ�����������true��
            _webView.CoreWebView2.Settings.AreDevToolsEnabled = false;

            // ��ֹ web ���ݷ���������������������true�����ע����C#����ΪJS���������Ϊtrue���С�
            _webView.CoreWebView2.Settings.AreHostObjectsAllowed = true;

            // �Ƿ���õ���ʧ�ܺ���Ⱦ����ʧ�ܵ����ô���ҳ�棬���ý���ʾ�հ�ҳ������������true��
            _webView.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = true;

            // �Ƿ�����һ������Ϣ��������Ϣ�ı�����Զ���д������������true��
            _webView.CoreWebView2.Settings.IsGeneralAutofillEnabled = true;

            // ��ֹ�Զ�����������Ϣ������������true��
            _webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;

            // ��ֹ֧�ִ���������豸��ʹ���󶯶���������WebView2�е���ҳ���ݣ�����������true��
            _webView.CoreWebView2.Settings.IsPinchZoomEnabled = false;

            // �Ƿ��������� JavaScript �ű�����Ӱ��ExecuteScriptAsync����ִ�нű�������������true��
            _webView.CoreWebView2.Settings.IsScriptEnabled = true;

            // �Ƿ���ʾ״̬��������������true��
            _webView.CoreWebView2.Settings.IsStatusBarEnabled = false;

            // �Ƿ���֧�ִ���������豸��ʹ��ˢ�����������WebView2������������true��
            _webView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;

            // �Ƿ������������WebView�Ķ���HTML�ĵ���ͨ�ţ�����������true��
            _webView.CoreWebView2.Settings.IsWebMessageEnabled = true;

            // �Ƿ�����ʹ�������ֺͼ��̲�����������WebView�ؼ��е����ݣ���Ӱ��ZoomFactor���ԣ�����������true��
            _webView.CoreWebView2.Settings.IsZoomControlEnabled = true;



            // �¼�ע��

            _webView.CoreWebView2.NewWindowRequested += (sender, e) =>
            {
                _webView.CoreWebView2.Navigate(e.Uri);
                e.Handled = true;
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
