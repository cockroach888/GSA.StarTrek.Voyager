namespace MSWebView4WinForm;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.PalMain = new System.Windows.Forms.Panel();
            this.PalToolbar = new System.Windows.Forms.Panel();
            this.TxtNavigate = new System.Windows.Forms.TextBox();
            this.TxtHostName = new System.Windows.Forms.TextBox();
            this.BtnDevTool = new System.Windows.Forms.Button();
            this.BtnNavigate = new System.Windows.Forms.Button();
            this.BtnMapping = new System.Windows.Forms.Button();
            this.TxtFolderPath = new System.Windows.Forms.TextBox();
            this.PalToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // PalMain
            // 
            this.PalMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalMain.Location = new System.Drawing.Point(0, 75);
            this.PalMain.Name = "PalMain";
            this.PalMain.Size = new System.Drawing.Size(1416, 761);
            this.PalMain.TabIndex = 1;
            // 
            // PalToolbar
            // 
            this.PalToolbar.Controls.Add(this.TxtNavigate);
            this.PalToolbar.Controls.Add(this.TxtHostName);
            this.PalToolbar.Controls.Add(this.BtnDevTool);
            this.PalToolbar.Controls.Add(this.BtnNavigate);
            this.PalToolbar.Controls.Add(this.BtnMapping);
            this.PalToolbar.Controls.Add(this.TxtFolderPath);
            this.PalToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PalToolbar.Location = new System.Drawing.Point(0, 0);
            this.PalToolbar.Name = "PalToolbar";
            this.PalToolbar.Size = new System.Drawing.Size(1416, 75);
            this.PalToolbar.TabIndex = 2;
            // 
            // TxtNavigate
            // 
            this.TxtNavigate.Location = new System.Drawing.Point(829, 21);
            this.TxtNavigate.Name = "TxtNavigate";
            this.TxtNavigate.Size = new System.Drawing.Size(475, 34);
            this.TxtNavigate.TabIndex = 2;
            this.TxtNavigate.Text = "https://sightx.com/1.html";
            // 
            // TxtHostName
            // 
            this.TxtHostName.Location = new System.Drawing.Point(377, 21);
            this.TxtHostName.Name = "TxtHostName";
            this.TxtHostName.Size = new System.Drawing.Size(200, 34);
            this.TxtHostName.TabIndex = 2;
            this.TxtHostName.Text = "sightx.com";
            // 
            // BtnDevTool
            // 
            this.BtnDevTool.Location = new System.Drawing.Point(677, 18);
            this.BtnDevTool.Name = "BtnDevTool";
            this.BtnDevTool.Size = new System.Drawing.Size(110, 40);
            this.BtnDevTool.TabIndex = 1;
            this.BtnDevTool.Text = "DevTool";
            this.BtnDevTool.UseVisualStyleBackColor = true;
            this.BtnDevTool.Click += new System.EventHandler(this.BtnDevTool_Click);
            // 
            // BtnNavigate
            // 
            this.BtnNavigate.Location = new System.Drawing.Point(1310, 18);
            this.BtnNavigate.Name = "BtnNavigate";
            this.BtnNavigate.Size = new System.Drawing.Size(85, 40);
            this.BtnNavigate.TabIndex = 1;
            this.BtnNavigate.Text = " 导航";
            this.BtnNavigate.UseVisualStyleBackColor = true;
            this.BtnNavigate.Click += new System.EventHandler(this.BtnNavigate_Click);
            // 
            // BtnMapping
            // 
            this.BtnMapping.Location = new System.Drawing.Point(587, 18);
            this.BtnMapping.Name = "BtnMapping";
            this.BtnMapping.Size = new System.Drawing.Size(85, 40);
            this.BtnMapping.TabIndex = 1;
            this.BtnMapping.Text = " 映射";
            this.BtnMapping.UseVisualStyleBackColor = true;
            this.BtnMapping.Click += new System.EventHandler(this.BtnMapping_Click);
            // 
            // TxtFolderPath
            // 
            this.TxtFolderPath.Location = new System.Drawing.Point(17, 21);
            this.TxtFolderPath.Name = "TxtFolderPath";
            this.TxtFolderPath.Size = new System.Drawing.Size(350, 34);
            this.TxtFolderPath.TabIndex = 0;
            this.TxtFolderPath.Text = "D:\\data\\LargeImages";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 836);
            this.Controls.Add(this.PalMain);
            this.Controls.Add(this.PalToolbar);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.PalToolbar.ResumeLayout(false);
            this.PalToolbar.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Panel PalMain;
    private Panel PalToolbar;
    private TextBox TxtHostName;
    private Button BtnMapping;
    private TextBox TxtFolderPath;
    private Button BtnDevTool;
    private TextBox TxtNavigate;
    private Button BtnNavigate;
}
