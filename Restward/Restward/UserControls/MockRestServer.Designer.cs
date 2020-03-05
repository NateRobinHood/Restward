namespace Restward.UserControls
{
    partial class MockRestServer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MockRestServer));
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.txtAuthToken = new System.Windows.Forms.TextBox();
            this.lblAuthCode = new System.Windows.Forms.Label();
            this.txtAuthPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtAuthUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.checkBoxAuth = new System.Windows.Forms.CheckBox();
            this.txtListener = new System.Windows.Forms.TextBox();
            this.txtBaseAddress = new System.Windows.Forms.TextBox();
            this.lblBaseAddress = new System.Windows.Forms.Label();
            this.lblListener = new System.Windows.Forms.Label();
            this.rbHttps = new System.Windows.Forms.RadioButton();
            this.rbHttp = new System.Windows.Forms.RadioButton();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.cmdPopOut = new System.Windows.Forms.Button();
            this.cmdClearLog = new System.Windows.Forms.Button();
            this.scEndpoints = new System.Windows.Forms.SplitContainer();
            this.toolStripEndpoints = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddEndpoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveEndpoint = new System.Windows.Forms.ToolStripButton();
            this.lvEndpoints = new Restward.CustomDrawnListView();
            this.tabControlEndpoints = new Restward.CustomDrawnTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbSettings.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scEndpoints)).BeginInit();
            this.scEndpoints.Panel1.SuspendLayout();
            this.scEndpoints.Panel2.SuspendLayout();
            this.scEndpoints.SuspendLayout();
            this.toolStripEndpoints.SuspendLayout();
            this.tabControlEndpoints.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSettings
            // 
            this.gbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSettings.Controls.Add(this.txtAuthToken);
            this.gbSettings.Controls.Add(this.lblAuthCode);
            this.gbSettings.Controls.Add(this.txtAuthPassword);
            this.gbSettings.Controls.Add(this.lblPassword);
            this.gbSettings.Controls.Add(this.txtAuthUser);
            this.gbSettings.Controls.Add(this.lblUser);
            this.gbSettings.Controls.Add(this.checkBoxAuth);
            this.gbSettings.Controls.Add(this.txtListener);
            this.gbSettings.Controls.Add(this.txtBaseAddress);
            this.gbSettings.Controls.Add(this.lblBaseAddress);
            this.gbSettings.Controls.Add(this.lblListener);
            this.gbSettings.Controls.Add(this.rbHttps);
            this.gbSettings.Controls.Add(this.rbHttp);
            this.gbSettings.Controls.Add(this.lblProtocol);
            this.gbSettings.Controls.Add(this.txtPort);
            this.gbSettings.Controls.Add(this.lblPort);
            this.gbSettings.Location = new System.Drawing.Point(3, 3);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(342, 225);
            this.gbSettings.TabIndex = 0;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Mock Rest Service Settings";
            // 
            // txtAuthToken
            // 
            this.txtAuthToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthToken.Location = new System.Drawing.Point(75, 194);
            this.txtAuthToken.Name = "txtAuthToken";
            this.txtAuthToken.ReadOnly = true;
            this.txtAuthToken.Size = new System.Drawing.Size(261, 20);
            this.txtAuthToken.TabIndex = 15;
            // 
            // lblAuthCode
            // 
            this.lblAuthCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAuthCode.Location = new System.Drawing.Point(6, 194);
            this.lblAuthCode.Name = "lblAuthCode";
            this.lblAuthCode.Size = new System.Drawing.Size(69, 20);
            this.lblAuthCode.TabIndex = 14;
            this.lblAuthCode.Text = "Auth Token";
            this.lblAuthCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAuthPassword
            // 
            this.txtAuthPassword.Location = new System.Drawing.Point(65, 167);
            this.txtAuthPassword.Name = "txtAuthPassword";
            this.txtAuthPassword.Size = new System.Drawing.Size(194, 20);
            this.txtAuthPassword.TabIndex = 13;
            this.txtAuthPassword.TextChanged += new System.EventHandler(this.txtAuthPassword_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 170);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Password";
            // 
            // txtAuthUser
            // 
            this.txtAuthUser.Location = new System.Drawing.Point(41, 141);
            this.txtAuthUser.Name = "txtAuthUser";
            this.txtAuthUser.Size = new System.Drawing.Size(218, 20);
            this.txtAuthUser.TabIndex = 11;
            this.txtAuthUser.TextChanged += new System.EventHandler(this.txtAuthUser_TextChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(6, 144);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(29, 13);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "User";
            // 
            // checkBoxAuth
            // 
            this.checkBoxAuth.AutoSize = true;
            this.checkBoxAuth.Location = new System.Drawing.Point(6, 118);
            this.checkBoxAuth.Name = "checkBoxAuth";
            this.checkBoxAuth.Size = new System.Drawing.Size(116, 17);
            this.checkBoxAuth.TabIndex = 9;
            this.checkBoxAuth.Text = "Use Authentication";
            this.checkBoxAuth.UseVisualStyleBackColor = true;
            this.checkBoxAuth.CheckedChanged += new System.EventHandler(this.checkBoxAuth_CheckedChanged);
            // 
            // txtListener
            // 
            this.txtListener.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtListener.Location = new System.Drawing.Point(52, 92);
            this.txtListener.Name = "txtListener";
            this.txtListener.ReadOnly = true;
            this.txtListener.Size = new System.Drawing.Size(284, 20);
            this.txtListener.TabIndex = 8;
            // 
            // txtBaseAddress
            // 
            this.txtBaseAddress.Location = new System.Drawing.Point(81, 63);
            this.txtBaseAddress.Name = "txtBaseAddress";
            this.txtBaseAddress.Size = new System.Drawing.Size(255, 20);
            this.txtBaseAddress.TabIndex = 7;
            this.txtBaseAddress.TextChanged += new System.EventHandler(this.txtBaseAddress_TextChanged);
            // 
            // lblBaseAddress
            // 
            this.lblBaseAddress.AutoSize = true;
            this.lblBaseAddress.Location = new System.Drawing.Point(6, 66);
            this.lblBaseAddress.Name = "lblBaseAddress";
            this.lblBaseAddress.Size = new System.Drawing.Size(69, 13);
            this.lblBaseAddress.TabIndex = 6;
            this.lblBaseAddress.Text = "BaseAddress";
            // 
            // lblListener
            // 
            this.lblListener.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblListener.Location = new System.Drawing.Point(6, 92);
            this.lblListener.Name = "lblListener";
            this.lblListener.Size = new System.Drawing.Size(46, 20);
            this.lblListener.TabIndex = 5;
            this.lblListener.Text = "Listener";
            this.lblListener.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbHttps
            // 
            this.rbHttps.AutoSize = true;
            this.rbHttps.Location = new System.Drawing.Point(118, 14);
            this.rbHttps.Name = "rbHttps";
            this.rbHttps.Size = new System.Drawing.Size(61, 17);
            this.rbHttps.TabIndex = 4;
            this.rbHttps.TabStop = true;
            this.rbHttps.Text = "HTTPS";
            this.rbHttps.UseVisualStyleBackColor = true;
            // 
            // rbHttp
            // 
            this.rbHttp.AutoSize = true;
            this.rbHttp.Checked = true;
            this.rbHttp.Location = new System.Drawing.Point(58, 14);
            this.rbHttp.Name = "rbHttp";
            this.rbHttp.Size = new System.Drawing.Size(54, 17);
            this.rbHttp.TabIndex = 3;
            this.rbHttp.TabStop = true;
            this.rbHttp.Text = "HTTP";
            this.rbHttp.UseVisualStyleBackColor = true;
            this.rbHttp.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // lblProtocol
            // 
            this.lblProtocol.AutoSize = true;
            this.lblProtocol.Location = new System.Drawing.Point(6, 16);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(46, 13);
            this.lblProtocol.TabIndex = 2;
            this.lblProtocol.Text = "Protocol";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(38, 37);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(86, 20);
            this.txtPort.TabIndex = 1;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 40);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port";
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Location = new System.Drawing.Point(3, 257);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(345, 115);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "";
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStart,
            this.toolStripButtonStop,
            this.toolStripProgressBar});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1048, 25);
            this.toolStripMain.TabIndex = 3;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStart.Text = "toolStripButton1";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStop.Text = "toolStripButton2";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(300, 22);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar.Visible = false;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(0, 25);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.cmdPopOut);
            this.scMain.Panel1.Controls.Add(this.cmdClearLog);
            this.scMain.Panel1.Controls.Add(this.gbSettings);
            this.scMain.Panel1.Controls.Add(this.rtbLog);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scEndpoints);
            this.scMain.Size = new System.Drawing.Size(1048, 372);
            this.scMain.SplitterDistance = 353;
            this.scMain.TabIndex = 4;
            // 
            // cmdPopOut
            // 
            this.cmdPopOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPopOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdPopOut.Image = ((System.Drawing.Image)(resources.GetObject("cmdPopOut.Image")));
            this.cmdPopOut.Location = new System.Drawing.Point(322, 230);
            this.cmdPopOut.Name = "cmdPopOut";
            this.cmdPopOut.Size = new System.Drawing.Size(23, 23);
            this.cmdPopOut.TabIndex = 3;
            this.cmdPopOut.UseVisualStyleBackColor = true;
            this.cmdPopOut.Click += new System.EventHandler(this.cmdPopOut_Click);
            // 
            // cmdClearLog
            // 
            this.cmdClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClearLog.Image = ((System.Drawing.Image)(resources.GetObject("cmdClearLog.Image")));
            this.cmdClearLog.Location = new System.Drawing.Point(293, 230);
            this.cmdClearLog.Name = "cmdClearLog";
            this.cmdClearLog.Size = new System.Drawing.Size(23, 23);
            this.cmdClearLog.TabIndex = 2;
            this.cmdClearLog.UseVisualStyleBackColor = true;
            this.cmdClearLog.Click += new System.EventHandler(this.cmdClearLog_Click);
            // 
            // scEndpoints
            // 
            this.scEndpoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scEndpoints.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scEndpoints.Location = new System.Drawing.Point(0, 0);
            this.scEndpoints.Name = "scEndpoints";
            // 
            // scEndpoints.Panel1
            // 
            this.scEndpoints.Panel1.Controls.Add(this.lvEndpoints);
            this.scEndpoints.Panel1.Controls.Add(this.toolStripEndpoints);
            // 
            // scEndpoints.Panel2
            // 
            this.scEndpoints.Panel2.Controls.Add(this.tabControlEndpoints);
            this.scEndpoints.Size = new System.Drawing.Size(691, 372);
            this.scEndpoints.SplitterDistance = 177;
            this.scEndpoints.TabIndex = 3;
            // 
            // toolStripEndpoints
            // 
            this.toolStripEndpoints.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEndpoints.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddEndpoint,
            this.toolStripButtonRemoveEndpoint});
            this.toolStripEndpoints.Location = new System.Drawing.Point(0, 0);
            this.toolStripEndpoints.Name = "toolStripEndpoints";
            this.toolStripEndpoints.Size = new System.Drawing.Size(177, 25);
            this.toolStripEndpoints.TabIndex = 3;
            this.toolStripEndpoints.Text = "toolStrip1";
            // 
            // toolStripButtonAddEndpoint
            // 
            this.toolStripButtonAddEndpoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddEndpoint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddEndpoint.Image")));
            this.toolStripButtonAddEndpoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddEndpoint.Name = "toolStripButtonAddEndpoint";
            this.toolStripButtonAddEndpoint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddEndpoint.Text = "toolStripButton1";
            this.toolStripButtonAddEndpoint.Click += new System.EventHandler(this.toolStripButtonAddEndpoint_Click);
            // 
            // toolStripButtonRemoveEndpoint
            // 
            this.toolStripButtonRemoveEndpoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemoveEndpoint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemoveEndpoint.Image")));
            this.toolStripButtonRemoveEndpoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveEndpoint.Name = "toolStripButtonRemoveEndpoint";
            this.toolStripButtonRemoveEndpoint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRemoveEndpoint.Text = "toolStripButton1";
            this.toolStripButtonRemoveEndpoint.ToolTipText = "Remove Endpoint";
            this.toolStripButtonRemoveEndpoint.Click += new System.EventHandler(this.toolStripButtonRemoveEndpoint_Click);
            // 
            // lvEndpoints
            // 
            this.lvEndpoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEndpoints.HideSelection = false;
            this.lvEndpoints.Location = new System.Drawing.Point(0, 25);
            this.lvEndpoints.Name = "lvEndpoints";
            this.lvEndpoints.OwnerDraw = true;
            this.lvEndpoints.Size = new System.Drawing.Size(177, 347);
            this.lvEndpoints.TabIndex = 2;
            this.lvEndpoints.UseCompatibleStateImageBehavior = false;
            this.lvEndpoints.View = System.Windows.Forms.View.List;
            this.lvEndpoints.SelectedIndexChanged += new System.EventHandler(this.lvEndpoints_SelectedIndexChanged);
            this.lvEndpoints.DoubleClick += new System.EventHandler(this.lvEndpoints_DoubleClick);
            // 
            // tabControlEndpoints
            // 
            this.tabControlEndpoints.AllowClosingTabs = true;
            this.tabControlEndpoints.CloseButtonActive = null;
            this.tabControlEndpoints.CloseButtonInactive = null;
            this.tabControlEndpoints.Controls.Add(this.tabPage1);
            this.tabControlEndpoints.Controls.Add(this.tabPage2);
            this.tabControlEndpoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEndpoints.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlEndpoints.ItemSize = new System.Drawing.Size(75, 21);
            this.tabControlEndpoints.Location = new System.Drawing.Point(0, 0);
            this.tabControlEndpoints.Name = "tabControlEndpoints";
            this.tabControlEndpoints.SelectedIndex = 0;
            this.tabControlEndpoints.Size = new System.Drawing.Size(510, 372);
            this.tabControlEndpoints.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(502, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(502, 343);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MockRestServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.toolStripMain);
            this.Name = "MockRestServer";
            this.Size = new System.Drawing.Size(1048, 397);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scEndpoints.Panel1.ResumeLayout(false);
            this.scEndpoints.Panel1.PerformLayout();
            this.scEndpoints.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scEndpoints)).EndInit();
            this.scEndpoints.ResumeLayout(false);
            this.toolStripEndpoints.ResumeLayout(false);
            this.toolStripEndpoints.PerformLayout();
            this.tabControlEndpoints.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblProtocol;
        private System.Windows.Forms.RadioButton rbHttp;
        private System.Windows.Forms.RadioButton rbHttps;
        private System.Windows.Forms.TextBox txtListener;
        private System.Windows.Forms.TextBox txtBaseAddress;
        private System.Windows.Forms.Label lblBaseAddress;
        private System.Windows.Forms.Label lblListener;
        private System.Windows.Forms.RichTextBox rtbLog;
        private CustomDrawnListView lvEndpoints;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.SplitContainer scEndpoints;
        private CustomDrawnTabControl tabControlEndpoints;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStripEndpoints;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddEndpoint;
        private System.Windows.Forms.TextBox txtAuthToken;
        private System.Windows.Forms.Label lblAuthCode;
        private System.Windows.Forms.TextBox txtAuthPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtAuthUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.CheckBox checkBoxAuth;
        private System.Windows.Forms.Button cmdClearLog;
        private System.Windows.Forms.Button cmdPopOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveEndpoint;
    }
}
