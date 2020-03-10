namespace Restward.UserControls
{
    partial class MockEndpoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MockEndpoint));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.comboBoxStatusCode = new System.Windows.Forms.ComboBox();
            this.comboBoxContentType = new System.Windows.Forms.ComboBox();
            this.rtbResponse = new System.Windows.Forms.RichTextBox();
            this.comboBoxHttpMethod = new System.Windows.Forms.ComboBox();
            this.listViewResponses = new Restward.CustomDrawnListView();
            this.lblHttpMethod = new System.Windows.Forms.Label();
            this.lblResponseCode = new System.Windows.Forms.Label();
            this.lblContentType = new System.Windows.Forms.Label();
            this.checkBoxReponseDelay = new System.Windows.Forms.CheckBox();
            this.txtResponseDelay = new System.Windows.Forms.TextBox();
            this.lblMs = new System.Windows.Forms.Label();
            this.groupBoxMatch = new System.Windows.Forms.GroupBox();
            this.groupBoxReponse = new System.Windows.Forms.GroupBox();
            this.lblListenerAddress = new System.Windows.Forms.Label();
            this.toolStripButtonRemovePattern = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
            this.groupBoxMatch.SuspendLayout();
            this.groupBoxReponse.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonRemovePattern});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(606, 25);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAdd.Text = "toolStripButton1";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Location = new System.Drawing.Point(0, 28);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(606, 20);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Text = "http://localhost:8080/";
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // comboBoxStatusCode
            // 
            this.comboBoxStatusCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatusCode.FormattingEnabled = true;
            this.comboBoxStatusCode.Location = new System.Drawing.Point(95, 13);
            this.comboBoxStatusCode.Name = "comboBoxStatusCode";
            this.comboBoxStatusCode.Size = new System.Drawing.Size(189, 21);
            this.comboBoxStatusCode.TabIndex = 3;
            this.comboBoxStatusCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatusCode_SelectedIndexChanged);
            // 
            // comboBoxContentType
            // 
            this.comboBoxContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxContentType.FormattingEnabled = true;
            this.comboBoxContentType.Location = new System.Drawing.Point(95, 40);
            this.comboBoxContentType.Name = "comboBoxContentType";
            this.comboBoxContentType.Size = new System.Drawing.Size(189, 21);
            this.comboBoxContentType.TabIndex = 4;
            this.comboBoxContentType.SelectedIndexChanged += new System.EventHandler(this.comboBoxContentType_SelectedIndexChanged);
            // 
            // rtbResponse
            // 
            this.rtbResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbResponse.Location = new System.Drawing.Point(0, 232);
            this.rtbResponse.Name = "rtbResponse";
            this.rtbResponse.Size = new System.Drawing.Size(606, 188);
            this.rtbResponse.TabIndex = 5;
            this.rtbResponse.Text = "";
            this.rtbResponse.TextChanged += new System.EventHandler(this.rtbResponse_TextChanged);
            // 
            // comboBoxHttpMethod
            // 
            this.comboBoxHttpMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHttpMethod.FormattingEnabled = true;
            this.comboBoxHttpMethod.Location = new System.Drawing.Point(94, 13);
            this.comboBoxHttpMethod.Name = "comboBoxHttpMethod";
            this.comboBoxHttpMethod.Size = new System.Drawing.Size(189, 21);
            this.comboBoxHttpMethod.TabIndex = 7;
            this.comboBoxHttpMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxHttpMethod_SelectedIndexChanged);
            // 
            // listViewResponses
            // 
            this.listViewResponses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResponses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewResponses.HideSelection = false;
            this.listViewResponses.Location = new System.Drawing.Point(0, 66);
            this.listViewResponses.Name = "listViewResponses";
            this.listViewResponses.OwnerDraw = true;
            this.listViewResponses.Size = new System.Drawing.Size(301, 160);
            this.listViewResponses.TabIndex = 6;
            this.listViewResponses.UseCompatibleStateImageBehavior = false;
            this.listViewResponses.View = System.Windows.Forms.View.List;
            this.listViewResponses.SelectedIndexChanged += new System.EventHandler(this.listViewResponses_SelectedIndexChanged);
            // 
            // lblHttpMethod
            // 
            this.lblHttpMethod.AutoSize = true;
            this.lblHttpMethod.Location = new System.Drawing.Point(6, 16);
            this.lblHttpMethod.Name = "lblHttpMethod";
            this.lblHttpMethod.Size = new System.Drawing.Size(66, 13);
            this.lblHttpMethod.TabIndex = 8;
            this.lblHttpMethod.Text = "Http Method";
            // 
            // lblResponseCode
            // 
            this.lblResponseCode.AutoSize = true;
            this.lblResponseCode.Location = new System.Drawing.Point(6, 16);
            this.lblResponseCode.Name = "lblResponseCode";
            this.lblResponseCode.Size = new System.Drawing.Size(83, 13);
            this.lblResponseCode.TabIndex = 9;
            this.lblResponseCode.Text = "Response Code";
            // 
            // lblContentType
            // 
            this.lblContentType.AutoSize = true;
            this.lblContentType.Location = new System.Drawing.Point(7, 43);
            this.lblContentType.Name = "lblContentType";
            this.lblContentType.Size = new System.Drawing.Size(71, 13);
            this.lblContentType.TabIndex = 10;
            this.lblContentType.Text = "Content Type";
            // 
            // checkBoxReponseDelay
            // 
            this.checkBoxReponseDelay.AutoSize = true;
            this.checkBoxReponseDelay.Location = new System.Drawing.Point(10, 69);
            this.checkBoxReponseDelay.Name = "checkBoxReponseDelay";
            this.checkBoxReponseDelay.Size = new System.Drawing.Size(104, 17);
            this.checkBoxReponseDelay.TabIndex = 11;
            this.checkBoxReponseDelay.Text = "Response Delay";
            this.checkBoxReponseDelay.UseVisualStyleBackColor = true;
            this.checkBoxReponseDelay.CheckedChanged += new System.EventHandler(this.checkBoxReponseDelay_CheckedChanged);
            // 
            // txtResponseDelay
            // 
            this.txtResponseDelay.Location = new System.Drawing.Point(120, 67);
            this.txtResponseDelay.Name = "txtResponseDelay";
            this.txtResponseDelay.Size = new System.Drawing.Size(138, 20);
            this.txtResponseDelay.TabIndex = 12;
            this.txtResponseDelay.TextChanged += new System.EventHandler(this.txtResponseDelay_TextChanged);
            // 
            // lblMs
            // 
            this.lblMs.AutoSize = true;
            this.lblMs.Location = new System.Drawing.Point(264, 70);
            this.lblMs.Name = "lblMs";
            this.lblMs.Size = new System.Drawing.Size(20, 13);
            this.lblMs.TabIndex = 13;
            this.lblMs.Text = "ms";
            // 
            // groupBoxMatch
            // 
            this.groupBoxMatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMatch.Controls.Add(this.comboBoxHttpMethod);
            this.groupBoxMatch.Controls.Add(this.lblHttpMethod);
            this.groupBoxMatch.Location = new System.Drawing.Point(307, 70);
            this.groupBoxMatch.Name = "groupBoxMatch";
            this.groupBoxMatch.Size = new System.Drawing.Size(296, 49);
            this.groupBoxMatch.TabIndex = 14;
            this.groupBoxMatch.TabStop = false;
            this.groupBoxMatch.Text = "Match Conditions";
            // 
            // groupBoxReponse
            // 
            this.groupBoxReponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxReponse.Controls.Add(this.lblResponseCode);
            this.groupBoxReponse.Controls.Add(this.comboBoxStatusCode);
            this.groupBoxReponse.Controls.Add(this.lblMs);
            this.groupBoxReponse.Controls.Add(this.comboBoxContentType);
            this.groupBoxReponse.Controls.Add(this.txtResponseDelay);
            this.groupBoxReponse.Controls.Add(this.lblContentType);
            this.groupBoxReponse.Controls.Add(this.checkBoxReponseDelay);
            this.groupBoxReponse.Location = new System.Drawing.Point(307, 125);
            this.groupBoxReponse.Name = "groupBoxReponse";
            this.groupBoxReponse.Size = new System.Drawing.Size(296, 101);
            this.groupBoxReponse.TabIndex = 15;
            this.groupBoxReponse.TabStop = false;
            this.groupBoxReponse.Text = "Response";
            // 
            // lblListenerAddress
            // 
            this.lblListenerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListenerAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblListenerAddress.Location = new System.Drawing.Point(0, 47);
            this.lblListenerAddress.Name = "lblListenerAddress";
            this.lblListenerAddress.Size = new System.Drawing.Size(606, 20);
            this.lblListenerAddress.TabIndex = 16;
            this.lblListenerAddress.Text = "http://localhost:55000/";
            this.lblListenerAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripButtonRemovePattern
            // 
            this.toolStripButtonRemovePattern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemovePattern.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemovePattern.Image")));
            this.toolStripButtonRemovePattern.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemovePattern.Name = "toolStripButtonRemovePattern";
            this.toolStripButtonRemovePattern.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRemovePattern.Text = "toolStripButton1";
            this.toolStripButtonRemovePattern.ToolTipText = "Remove Pattern";
            this.toolStripButtonRemovePattern.Click += new System.EventHandler(this.toolStripButtonRemovePattern_Click);
            // 
            // MockEndpoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblListenerAddress);
            this.Controls.Add(this.groupBoxReponse);
            this.Controls.Add(this.groupBoxMatch);
            this.Controls.Add(this.listViewResponses);
            this.Controls.Add(this.rtbResponse);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.toolStripMain);
            this.Name = "MockEndpoint";
            this.Size = new System.Drawing.Size(606, 420);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.groupBoxMatch.ResumeLayout(false);
            this.groupBoxMatch.PerformLayout();
            this.groupBoxReponse.ResumeLayout(false);
            this.groupBoxReponse.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ComboBox comboBoxStatusCode;
        private System.Windows.Forms.ComboBox comboBoxContentType;
        private System.Windows.Forms.RichTextBox rtbResponse;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private CustomDrawnListView listViewResponses;
        private System.Windows.Forms.ComboBox comboBoxHttpMethod;
        private System.Windows.Forms.Label lblHttpMethod;
        private System.Windows.Forms.Label lblResponseCode;
        private System.Windows.Forms.Label lblContentType;
        private System.Windows.Forms.CheckBox checkBoxReponseDelay;
        private System.Windows.Forms.TextBox txtResponseDelay;
        private System.Windows.Forms.Label lblMs;
        private System.Windows.Forms.GroupBox groupBoxMatch;
        private System.Windows.Forms.GroupBox groupBoxReponse;
        private System.Windows.Forms.Label lblListenerAddress;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemovePattern;
    }
}
