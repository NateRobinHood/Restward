using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restward.Dialogs;
using Restward.Components;
using System.Reflection;
using System.IO;
using Restward.Data;

namespace Restward.UserControls
{
    public partial class MockRestServer : UserControl
    {
        private RestServerData m_RestServerData;
        private LogDialog m_LogDialog = null;
        private bool m_RestServerRunning = false;

        public MockRestServer(RestServerData data)
        {
            InitializeComponent();

            m_RestServerData = data;

            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(Color.FromArgb(195, 195, 195), Color.FromArgb(110, 110, 110));
            GradientRenderer.RoundedEdges = false;

            toolStripMain.Renderer = GradientRenderer;
            toolStripEndpoints.Renderer = GradientRenderer;

            Assembly ThisAssembly = Assembly.GetExecutingAssembly();
            Stream XButtonRedImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.CloseRed.png");
            Stream XButtonGrayImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.CloseGray.png");
            Image XButtonRedImage = Image.FromStream(XButtonRedImageStream);
            Image XButtonGrayImage = Image.FromStream(XButtonGrayImageStream);

            Stream WorkspaceImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.WorkspaceBackground.png");
            Image WorkspaceImage = Image.FromStream(WorkspaceImageStream);

            this.tabControlEndpoints.CloseButtonActive = XButtonRedImage;
            this.tabControlEndpoints.CloseButtonInactive = XButtonGrayImage;

            //this.scMain.Panel2.BackgroundImage = WorkspaceImage;
            //this.scMain.Panel2.BackgroundImageLayout = ImageLayout.Tile;

            this.scEndpoints.Panel2.BackgroundImage = WorkspaceImage;
            this.scEndpoints.Panel2.BackgroundImageLayout = ImageLayout.Tile;

            txtPort.Text = data.Port.ToString();
            txtBaseAddress.Text = data.BaseAddress;
            switch (data.Protocol)
            {
                case HttpProtocol.HTTP:
                    rbHttp.Checked = true;
                    break;
                case HttpProtocol.HTTPS:
                    rbHttps.Checked = true;
                    break;
            }

            UpdateListener();

            SetAuthEnables(false);

            tabControlEndpoints.TabPages.Clear();

            if (data.Endpoints.Count() > 0)
            {
                foreach (EndpointData endpoint in data.Endpoints)
                {
                    AddEndpoint(endpoint);
                }
            }

            m_RestServerData.OnEndpointAdded += RestServerData_OnEndpointAdded;
            m_RestServerData.OnEndpointRemoved += RestServerData_OnEndpointRemoved;
            m_RestServerData.OnEndpointChanged += RestServerData_OnEndpointChanged;
            ProjectData.RestServerManager.OnRestServerTrace += RestServerManager_OnRestServerTrace;
            ProjectData.RestServerManager.OnRestServerStarted += RestServerManager_OnRestServerStarted;
            ProjectData.RestServerManager.OnRestServerStopped += RestServerManager_OnRestServerStopped;
        }

        private void UpdateListener()
        {
            txtListener.Text = m_RestServerData.ListenerAddress;
        }

        private void SetAuthEnables(bool enable)
        {
            txtAuthUser.Enabled = enable;
            txtAuthPassword.Enabled = enable;
        }

        private void SetRestServerEnables(bool enable)
        {
            rbHttp.Enabled = enable;
            rbHttps.Enabled = enable;
            txtPort.Enabled = enable;
            txtBaseAddress.Enabled = enable;
            checkBoxAuth.Enabled = enable;
            txtAuthUser.Enabled = enable;
            txtAuthPassword.Enabled = enable;
            toolStripButtonAddEndpoint.Enabled = enable;
            toolStripButtonRemoveEndpoint.Enabled = enable;
            toolStripButtonStart.Enabled = enable;
            toolStripButtonStop.Enabled = !enable;
            toolStripProgressBar.Visible = !enable;

            foreach (TabPage tabPage in tabControlEndpoints.TabPages)
            {
                if(tabPage is MockEndpointTabPage)
                {
                    MockEndpointTabPage mockEndpointTabPage = tabPage as MockEndpointTabPage;
                    mockEndpointTabPage.MockEndpointUserControl.SetRestServerEnables(enable);
                }
            }
        }

        private void ShowEndpointTab(ListViewItem item, EndpointData data, bool openTab)
        {
            MockEndpointTabPage thisTagPage = tabControlEndpoints.TabPages.Cast<MockEndpointTabPage>().Where(c => c.EndpointData == data).FirstOrDefault();
            if (!m_RestServerData.Endpoints.Contains(data))
            {
                m_RestServerData.AddEndpoint(data);
            }
            else if (thisTagPage != null)
            {
                tabControlEndpoints.SelectedTab = thisTagPage;
            }
            else if(openTab)
            {
                MockEndpointTabPage newMockEndpointTabPage = new MockEndpointTabPage(data);

                item.Tag = data;

                newMockEndpointTabPage.Tag = item;
                tabControlEndpoints.TabPages.Add(newMockEndpointTabPage);

                tabControlEndpoints.SelectedTab = newMockEndpointTabPage;
            }
        }

        private void AddEndpoint(EndpointData data)
        {
            ListViewItem newMockEndpointItem = new ListViewItem(data.Name);
            MockEndpointTabPage newMockEndpointTabPage = new MockEndpointTabPage(data);

            newMockEndpointItem.Tag = data;
            lvEndpoints.Items.Add(newMockEndpointItem);

            newMockEndpointItem.Text = $"{data.Name}({data.HttpEndpointAddress}          )";
            newMockEndpointItem.Text = $"{data.Name}({data.HttpEndpointAddress})";

            newMockEndpointTabPage.Tag = newMockEndpointItem;
            tabControlEndpoints.TabPages.Add(newMockEndpointTabPage);
        }

        public RestServerData RestServerData
        {
            get
            {
                return m_RestServerData;
            }
        }

        public bool RestServerRunning
        {
            get
            {
                return m_RestServerRunning;
            }
        }

        //Event Handlers
        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPort.Text) && txtPort.Text != m_RestServerData.Port.ToString())
            {
                int tempPort = m_RestServerData.Port;
                int outPort;
                if (!int.TryParse(txtPort.Text, out outPort))
                {
                    txtPort.Text = tempPort.ToString();
                    txtPort.SelectionStart = txtPort.Text.Length;
                    txtPort.SelectionLength = 0;
                }
                else
                {
                    m_RestServerData.Port = outPort;
                }

                UpdateListener();
            }
        }

        private void txtBaseAddress_TextChanged(object sender, EventArgs e)
        {
            if (m_RestServerData.BaseAddress != txtBaseAddress.Text)
            {
                m_RestServerData.BaseAddress = txtBaseAddress.Text;

                UpdateListener();
            }
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHttp.Checked)
            {
                m_RestServerData.Protocol = HttpProtocol.HTTP;
            }
            else if (rbHttps.Checked)
            {
                m_RestServerData.Protocol = HttpProtocol.HTTPS;
            }

            UpdateListener();
        }

        private void lvEndpoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEndpoints.SelectedItems.Count > 1)
            {
                if (!m_RestServerRunning)
                {
                    toolStripButtonRemoveEndpoint.Enabled = true;
                }
            }
            else if (lvEndpoints.SelectedItems.Count == 1)
            {
                ListViewItem thisItem = lvEndpoints.SelectedItems[0];
                EndpointData thisData = thisItem.Tag as EndpointData;
                if (thisData != null)
                {
                    ShowEndpointTab(thisItem, thisData, false);
                }

                if (!m_RestServerRunning)
                {
                    toolStripButtonRemoveEndpoint.Enabled = true;
                }
            }
            else
            {
                toolStripButtonRemoveEndpoint.Enabled = false;
            }
        }

        private void lvEndpoints_DoubleClick(object sender, EventArgs e)
        {
            if (lvEndpoints.SelectedItems.Count == 1)
            {
                ListViewItem thisItem = lvEndpoints.SelectedItems[0];
                EndpointData thisData = thisItem.Tag as EndpointData;
                if (thisData != null)
                {
                    ShowEndpointTab(thisItem, thisData, true);
                }
            }
        }

        private void RestServerData_OnEndpointRemoved(object sender, OnEndpointRemovedEventArgs e)
        {
            MockEndpointTabPage thisTabPage = tabControlEndpoints.TabPages.Cast<MockEndpointTabPage>().Where(c => c.MockEndpointUserControl.EndpointData == e.Endpoint).FirstOrDefault();
            tabControlEndpoints.TabPages.Remove(thisTabPage);
        }

        private void RestServerData_OnEndpointAdded(object sender, OnEndpointAddedEventArgs e)
        {
            AddEndpoint(e.Endpoint);
        }

        private void RestServerData_OnEndpointChanged(object sender, OnEndpointChangedEventArgs e)
        {
            if (e.PropertyName == "HttpEndpointAddress" || e.PropertyName == "Name")
            {
                ListViewItem thisItem = lvEndpoints.Items.Cast<ListViewItem>().Where(c => c.Tag == e.Endpoint).FirstOrDefault();
                if (thisItem != null)
                {
                    thisItem.Text = $"{e.Endpoint.Name}({e.Endpoint.HttpEndpointAddress}          )";
                    thisItem.Text = $"{e.Endpoint.Name}({e.Endpoint.HttpEndpointAddress})";
                }
            }
        }

        private void RestServerManager_OnRestServerTrace(object sender, OnRestServerTraceEventArgs e)
        {
            if (e.RestServerData == m_RestServerData)
            {
                if (rtbLog.InvokeRequired)
                {
                    rtbLog.BeginInvoke(new Action(() => 
                    {
                        rtbLog.AppendText(e.Message);
                        rtbLog.AppendText(Environment.NewLine);
                        rtbLog.SelectionStart = rtbLog.Text.Length;
                        rtbLog.ScrollToCaret();
                    }));
                }
                else
                {
                    rtbLog.AppendText(e.Message);
                    rtbLog.AppendText(Environment.NewLine);
                    rtbLog.SelectionStart = rtbLog.Text.Length;
                    rtbLog.ScrollToCaret();
                }
            }
        }

        private void RestServerManager_OnRestServerStopped(object sender, OnRestServerStoppedEventArgs e)
        {
            if (e.RestServer == m_RestServerData)
            {
                m_RestServerRunning = false;
                SetRestServerEnables(true);
            }
        }

        private void RestServerManager_OnRestServerStarted(object sender, OnRestServerStartedEventArgs e)
        {
            if (e.RestServer == m_RestServerData)
            {
                m_RestServerRunning = true;
                SetRestServerEnables(false);
            }
        }

        private void checkBoxAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAuth.Checked)
            {
                m_RestServerData.UseAuth = true;
                SetAuthEnables(true);
            }
            else
            {
                m_RestServerData.UseAuth = false;
                SetAuthEnables(false);
            }
        }

        private void txtAuthUser_TextChanged(object sender, EventArgs e)
        {
            string tempUser = m_RestServerData.AuthUser;
            try
            {
                if (m_RestServerData.AuthUser != txtAuthUser.Text)
                {
                    m_RestServerData.AuthUser = txtAuthUser.Text;
                }
            }
            catch
            {
                //revert
                m_RestServerData.AuthUser = tempUser;
                txtAuthUser.Text = tempUser;
                txtAuthUser.SelectionStart = txtAuthUser.Text.Length;
                txtAuthUser.SelectionLength = 0;
            }
        }

        private void txtAuthPassword_TextChanged(object sender, EventArgs e)
        {
            string tempPassword = m_RestServerData.AuthPassword;
            try
            {
                if (m_RestServerData.AuthPassword != txtAuthPassword.Text)
                {
                    m_RestServerData.AuthPassword = txtAuthPassword.Text;
                }
            }
            catch
            {
                //revert
                m_RestServerData.AuthPassword = tempPassword;
                txtAuthPassword.Text = tempPassword;
                txtAuthPassword.SelectionStart = txtAuthPassword.Text.Length;
                txtAuthPassword.SelectionLength = 0;
            }
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            ProjectData.RestServerManager.StartServer(m_RestServerData);
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            ProjectData.RestServerManager.StopServer(m_RestServerData);
        }

        private void cmdClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }

        private void cmdPopOut_Click(object sender, EventArgs e)
        {
            if (m_LogDialog == null)
                m_LogDialog = new LogDialog(m_RestServerData);

            m_LogDialog.Show();
        }

        private void toolStripButtonRemoveEndpoint_Click(object sender, EventArgs e)
        {
            if (lvEndpoints.SelectedItems.Count > 1)
            {
                for (int i = 0; i < lvEndpoints.SelectedItems.Count; i++)
                {
                    ListViewItem thisItem = lvEndpoints.SelectedItems[i];
                    EndpointData thisData = thisItem.Tag as EndpointData;
                    if (MessageBox.Show($"Do you want to remove endpoint {thisData.Name}?", "Remove Endpoint", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (thisData != null)
                        {
                            m_RestServerData.RemoveEndpoint(thisData);
                        }

                        lvEndpoints.Items.Remove(thisItem);
                    }
                }
            }
            else if (lvEndpoints.SelectedItems.Count == 1)
            {
                ListViewItem thisItem = lvEndpoints.SelectedItems[0];
                EndpointData thisData = thisItem.Tag as EndpointData;
                if (MessageBox.Show($"Do you want to remove endpoint {thisData.Name}?", "Remove Endpoint", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (thisData != null)
                    {
                        m_RestServerData.RemoveEndpoint(thisData);
                    }

                    lvEndpoints.Items.Remove(thisItem);
                }
            }
        }

        private void toolStripButtonAddEndpoint_Click(object sender, EventArgs e)
        {
            using (AddMockEndpointDialog AMED = new AddMockEndpointDialog())
            {
                if (AMED.ShowDialog() == DialogResult.OK)
                {
                    EndpointData newData = new EndpointData(AMED.MockEndpointName);
                    m_RestServerData.AddEndpoint(newData);
                }
            }
        }
    }
}
