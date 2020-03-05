using Restward.Components;
using Restward.Data;
using Restward.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restward
{
    public partial class Form1 : Form
    {
        private TreeNode m_RootProjectNode;

        public Form1()
        {
            InitializeComponent();

#if (!DEBUG)
            if (!IsAdministrator())
            {
                string exeName = Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";

                Process.Start(startInfo);

                this.Close();
                Application.Exit();
                return;
            }
#endif

            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(Color.FromArgb(195, 195, 195), Color.FromArgb(110, 110, 110));
            GradientRenderer.RoundedEdges = false;
            MenuToolStripRenderer MenuRenderer = new MenuToolStripRenderer();

            menuStripMain.Renderer = MenuRenderer;
            toolStripServices.Renderer = GradientRenderer;

            Assembly ThisAssembly = Assembly.GetExecutingAssembly();
            Stream XButtonRedImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.CloseRed.png");
            Stream XButtonGrayImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.CloseGray.png");
            Image XButtonRedImage = Image.FromStream(XButtonRedImageStream);
            Image XButtonGrayImage = Image.FromStream(XButtonGrayImageStream);

            Stream TreeViewExpandedImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.TreeViewExpanded.png");
            Stream TreeViewCollapsedImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.TreeViewCollapsed.png");
            Image TreeViewExpandedImage = Image.FromStream(TreeViewExpandedImageStream);
            Image TreeViewCollapsedImage = Image.FromStream(TreeViewCollapsedImageStream);

            Stream WorkspaceImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.WorkspaceBackground.png");
            Image WorkspaceImage = Image.FromStream(WorkspaceImageStream);

            this.tabControlMain.CloseButtonActive = XButtonRedImage;
            this.tabControlMain.CloseButtonInactive = XButtonGrayImage;

            this.treeViewServices.ExpandedImage = TreeViewExpandedImage;
            this.treeViewServices.CollapsedImage = TreeViewCollapsedImage;
            //this.treeViewServices.DrawMode = TreeViewDrawMode.OwnerDrawAll;

            this.scMain.Panel2.BackgroundImage = WorkspaceImage;
            this.scMain.Panel2.BackgroundImageLayout = ImageLayout.Tile;

            m_RootProjectNode = new TreeNode("Project");
            treeViewServices.Nodes.Add(m_RootProjectNode);

            tabControlMain.TabPages.Clear();

            ProjectData.OnRestServerAdded += ProjectData_OnRestServerAdded;
            ProjectData.OnRestServerRemoved += ProjectData_OnRestServerRemoved;
            ProjectData.RestServerManager.OnRestServerStarted += RestServerManager_OnRestServerStarted;
            ProjectData.RestServerManager.OnRestServerStopped += RestServerManager_OnRestServerStopped;

            this.FormClosing += Form_FormClosing;
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        //Event Handlers
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            using (AddMockServiceDialog AMSD = new AddMockServiceDialog())
            {
                if (AMSD.ShowDialog() == DialogResult.OK)
                {
                    RestServerData newData = new RestServerData(AMSD.MockServiceName);
                    ProjectData.AddRestServer(newData);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog SFD = new SaveFileDialog())
            {
                SFD.Filter = "rwproj files (*.rwproj)|*.rwproj";
                SFD.RestoreDirectory = true;

                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(SFD.FileName, ProjectData.GetSaveFile());
                    ProjectData.ResetIsDirty();
                }
            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.Filter = "rwproj files (*.rwproj)|*.rwproj";
                OFD.RestoreDirectory = true;
                OFD.Multiselect = false;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    if (OFD.CheckFileExists)
                    {
                        ProjectData.LoadSaveFile(File.ReadAllText(OFD.FileName));
                    }
                    else
                    {
                        MessageBox.Show("The selected file was not found or is inaccessible", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ProjectData_OnRestServerRemoved(object sender, OnRestServerRemovedEventArgs e)
        {
            MockServiceTabPage thisTabPage = tabControlMain.TabPages.Cast<MockServiceTabPage>().Where(c => c.MockRestServerUserControl.RestServerData == e.RestServer).FirstOrDefault();
            if (thisTabPage != null)
            {
                tabControlMain.TabPages.Remove(thisTabPage);
            }
        }

        private void ProjectData_OnRestServerAdded(object sender, OnRestServerAddedEventArgs e)
        {
            TreeNode newMockServiceTreeNode = new TreeNode(e.RestServer.Name);
            MockServiceTabPage newMockServiceTabPage = new MockServiceTabPage(e.RestServer);

            newMockServiceTreeNode.Tag = newMockServiceTabPage;
            m_RootProjectNode.Nodes.Add(newMockServiceTreeNode);

            newMockServiceTabPage.Tag = newMockServiceTreeNode;
            tabControlMain.TabPages.Add(newMockServiceTabPage);
        }

        private void toolStripButtonRemoveServer_Click(object sender, EventArgs e)
        {
            if (treeViewServices.SelectedNode != null)
            {
                if (treeViewServices.SelectedNode.Tag is MockServiceTabPage)
                {
                    MockServiceTabPage thisTabPage = treeViewServices.SelectedNode.Tag as MockServiceTabPage;
                    if (thisTabPage != null && MessageBox.Show($"Do you want to remove rest server {thisTabPage.MockRestServerUserControl.RestServerData.Name}?", "Remove Rest Server", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ProjectData.RemoveRestServer(thisTabPage.MockRestServerUserControl.RestServerData);
                        treeViewServices.SelectedNode.Remove();
                    }
                }
            }
        }

        private void treeViewServices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewServices.SelectedNode != null)
            {
                if (treeViewServices.SelectedNode.Tag is MockServiceTabPage)
                {
                    MockServiceTabPage thisTabPage = treeViewServices.SelectedNode.Tag as MockServiceTabPage;
                    if (!thisTabPage.MockRestServerUserControl.RestServerRunning)
                    {
                        toolStripButtonRemoveServer.Enabled = true;
                    }
                }
                else
                {
                    toolStripButtonRemoveServer.Enabled = false;
                }
            }
            else
            {
                toolStripButtonRemoveServer.Enabled = false;
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (result == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void treeViewServices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo hitTest = treeViewServices.HitTest(e.Location);
            if (hitTest.Node != null && hitTest.Node.Tag is MockServiceTabPage)
            {
                MockServiceTabPage tabPage = hitTest.Node.Tag as MockServiceTabPage;
                if(!tabControlMain.TabPages.Contains(tabPage))
                    tabControlMain.TabPages.Add(tabPage);
            }
        }

        private void RestServerManager_OnRestServerStopped(object sender, OnRestServerStoppedEventArgs e)
        {
            if (treeViewServices.SelectedNode != null && treeViewServices.SelectedNode.Tag is MockServiceTabPage)
            {
                MockServiceTabPage thisTabPage = treeViewServices.SelectedNode.Tag as MockServiceTabPage;
                if (thisTabPage.MockRestServerUserControl.RestServerData == e.RestServer)
                {
                    toolStripButtonRemoveServer.Enabled = true;
                }
            }
        }

        private void RestServerManager_OnRestServerStarted(object sender, OnRestServerStartedEventArgs e)
        {
            if (treeViewServices.SelectedNode != null && treeViewServices.SelectedNode.Tag is MockServiceTabPage)
            {
                MockServiceTabPage thisTabPage = treeViewServices.SelectedNode.Tag as MockServiceTabPage;
                if (thisTabPage.MockRestServerUserControl.RestServerData == e.RestServer)
                {
                    toolStripButtonRemoveServer.Enabled = false;
                }
            }
        }
    }
}
