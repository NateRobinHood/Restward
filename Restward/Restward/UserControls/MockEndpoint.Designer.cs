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
            this.listViewResponses = new Restward.CustomDrawnListView();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(423, 25);
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
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Location = new System.Drawing.Point(0, 28);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(423, 20);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Text = "http://localhost:8080/";
            // 
            // comboBoxStatusCode
            // 
            this.comboBoxStatusCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatusCode.FormattingEnabled = true;
            this.comboBoxStatusCode.Location = new System.Drawing.Point(0, 194);
            this.comboBoxStatusCode.Name = "comboBoxStatusCode";
            this.comboBoxStatusCode.Size = new System.Drawing.Size(155, 21);
            this.comboBoxStatusCode.TabIndex = 3;
            // 
            // comboBoxContentType
            // 
            this.comboBoxContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxContentType.FormattingEnabled = true;
            this.comboBoxContentType.Location = new System.Drawing.Point(161, 194);
            this.comboBoxContentType.Name = "comboBoxContentType";
            this.comboBoxContentType.Size = new System.Drawing.Size(168, 21);
            this.comboBoxContentType.TabIndex = 4;
            // 
            // rtbResponse
            // 
            this.rtbResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbResponse.Location = new System.Drawing.Point(0, 216);
            this.rtbResponse.Name = "rtbResponse";
            this.rtbResponse.Size = new System.Drawing.Size(423, 204);
            this.rtbResponse.TabIndex = 5;
            this.rtbResponse.Text = "";
            // 
            // listViewResponses
            // 
            this.listViewResponses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewResponses.Location = new System.Drawing.Point(0, 47);
            this.listViewResponses.Name = "listViewResponses";
            this.listViewResponses.OwnerDraw = true;
            this.listViewResponses.Size = new System.Drawing.Size(423, 147);
            this.listViewResponses.TabIndex = 6;
            this.listViewResponses.UseCompatibleStateImageBehavior = false;
            this.listViewResponses.View = System.Windows.Forms.View.List;
            // 
            // MockEndpoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewResponses);
            this.Controls.Add(this.rtbResponse);
            this.Controls.Add(this.comboBoxContentType);
            this.Controls.Add(this.comboBoxStatusCode);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.toolStripMain);
            this.Name = "MockEndpoint";
            this.Size = new System.Drawing.Size(423, 420);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
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
    }
}
