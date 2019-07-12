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

namespace Restward.UserControls
{
    public partial class MockRestServer : UserControl
    {
        private int m_Port = 8080;
        private string m_BaseAddress = string.Empty;

        public MockRestServer()
        {
            InitializeComponent();

            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(Color.FromArgb(195, 195, 195), Color.FromArgb(110, 110, 110));
            GradientRenderer.RoundedEdges = false;

            toolStripMain.Renderer = GradientRenderer;

            txtPort.Text = m_Port.ToString();
            txtBaseAddress.Text = m_BaseAddress;

            UpdateListener();

            tabControlEndpoints.TabPages.Clear();
        }

        private string ListenerText
        {
            get
            {
                if (rbHttp.Checked)
                {
                    return $"http://localhost:{m_Port}/{m_BaseAddress}";
                }
                else if (rbHttps.Checked)
                {
                    return $"https://localhost:{m_Port}/{m_BaseAddress}";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private void UpdateListener()
        {
            txtListener.Text = ListenerText;
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPort.Text) && txtPort.Text != m_Port.ToString())
            {
                int tempPort = m_Port;
                if (!int.TryParse(txtPort.Text, out m_Port))
                {
                    txtPort.Text = tempPort.ToString();
                    txtPort.SelectionStart = txtPort.Text.Length;
                    txtPort.SelectionLength = 0;
                }

                UpdateListener();
            }
        }

        private void txtBaseAddress_TextChanged(object sender, EventArgs e)
        {
            if (m_BaseAddress != txtBaseAddress.Text)
            {
                m_BaseAddress = txtBaseAddress.Text;

                UpdateListener();
            }
        }

        private void rbHttp_CheckedChanged(object sender, EventArgs e)
        {
            UpdateListener();
        }

        private void toolStripEndpoints_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            using (AddMockEndpointDialog AMED = new AddMockEndpointDialog())
            {
                if (AMED.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem newMockEndpointItem = new ListViewItem(AMED.MockEndpointName);
                    MockEndpointTabPage newMockEndpointTabPage = new MockEndpointTabPage(AMED.MockEndpointName);

                    newMockEndpointItem.Tag = newMockEndpointTabPage;
                    lvEndpoints.Items.Add(newMockEndpointItem);

                    newMockEndpointTabPage.Tag = newMockEndpointItem;
                    tabControlEndpoints.TabPages.Add(newMockEndpointTabPage);
                }
            }
        }
    }
}
