namespace Restward
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.treeViewServices = new Restward.CustomDrawnTreeView();
            this.toolStripServices = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveServer = new System.Windows.Forms.ToolStripButton();
            this.tabControlMain = new Restward.CustomDrawnTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.toolStripServices.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(0, 24);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.treeViewServices);
            this.scMain.Panel1.Controls.Add(this.toolStripServices);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.tabControlMain);
            this.scMain.Size = new System.Drawing.Size(1072, 457);
            this.scMain.SplitterDistance = 199;
            this.scMain.TabIndex = 0;
            // 
            // treeViewServices
            // 
            this.treeViewServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewServices.CollapsedImage = null;
            this.treeViewServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewServices.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewServices.ExpandedImage = null;
            this.treeViewServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.treeViewServices.ItemHeight = 17;
            this.treeViewServices.Location = new System.Drawing.Point(0, 25);
            this.treeViewServices.Name = "treeViewServices";
            this.treeViewServices.Size = new System.Drawing.Size(199, 432);
            this.treeViewServices.TabIndex = 0;
            this.treeViewServices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewServices_AfterSelect);
            this.treeViewServices.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewServices_MouseDoubleClick);
            // 
            // toolStripServices
            // 
            this.toolStripServices.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripServices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonRemoveServer});
            this.toolStripServices.Location = new System.Drawing.Point(0, 0);
            this.toolStripServices.Name = "toolStripServices";
            this.toolStripServices.Size = new System.Drawing.Size(199, 25);
            this.toolStripServices.TabIndex = 1;
            this.toolStripServices.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAdd.ToolTipText = "Add Rest Server";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonRemoveServer
            // 
            this.toolStripButtonRemoveServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemoveServer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemoveServer.Image")));
            this.toolStripButtonRemoveServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveServer.Name = "toolStripButtonRemoveServer";
            this.toolStripButtonRemoveServer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRemoveServer.ToolTipText = "Remove Rest Server";
            this.toolStripButtonRemoveServer.Click += new System.EventHandler(this.toolStripButtonRemoveServer_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.AllowClosingTabs = true;
            this.tabControlMain.CloseButtonActive = null;
            this.tabControlMain.CloseButtonInactive = null;
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlMain.ItemSize = new System.Drawing.Size(75, 21);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(869, 457);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(861, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(861, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1072, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveToolStripMenuItem.Text = "Save Project";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 481);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "Form1";
            this.Text = "Restward - V1.1";
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel1.PerformLayout();
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.toolStripServices.ResumeLayout(false);
            this.toolStripServices.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private CustomDrawnTreeView treeViewServices;
        private CustomDrawnTabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripServices;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveServer;
    }
}

