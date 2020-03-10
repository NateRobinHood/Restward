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
using Restward.Data;

namespace Restward.UserControls
{
    public partial class MockEndpoint : UserControl
    {
        private EndpointData m_EndpointData;
        private PatternData m_SelectedPattern = null;
        private bool m_RestServerRunning = false;

        public MockEndpoint(EndpointData data)
        {
            InitializeComponent();

            m_EndpointData = data;

            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(Color.FromArgb(195, 195, 195), Color.FromArgb(110, 110, 110));
            GradientRenderer.RoundedEdges = false;

            toolStripMain.Renderer = GradientRenderer;

            comboBoxStatusCode.DataSource = DataEnums.GetEnumWrappers<HttpStatusCode>();
            comboBoxStatusCode.DisplayMember = "Descriptor";
            comboBoxStatusCode.ValueMember = "Value";

            comboBoxHttpMethod.DataSource = DataEnums.GetEnumWrappers<HttpMethod>();
            comboBoxHttpMethod.DisplayMember = "Descriptor";
            comboBoxHttpMethod.ValueMember = "Value";

            comboBoxContentType.DataSource = DataEnums.GetEnumWrappers<ContentType>();
            comboBoxContentType.DisplayMember = "Descriptor";
            comboBoxContentType.ValueMember = "Value";

            txtResponseDelay.Enabled = false;

            SetReponseEnables(false);

            if (data.Patterns.Count() > 0)
            {
                foreach (PatternData pattern in data.Patterns)
                {
                    AddPattern(pattern);
                }
            }


            if (data.HttpEndpointAddress != string.Empty)
            {
                txtAddress.Text = data.HttpEndpointAddress;
                lblListenerAddress.Text = m_EndpointData.Owner.ListenerAddress + data.HttpEndpointAddress;
            }
            else
            {
                lblListenerAddress.Text = m_EndpointData.Owner.ListenerAddress;
            }

            ProjectData.OnRestServerChanged += ProjectData_OnRestServerChanged;
            ProjectData.RestServerManager.OnRestServerStarted += RestServerManager_OnRestServerStarted;
            ProjectData.RestServerManager.OnRestServerStopped += RestServerManager_OnRestServerStopped;
            m_EndpointData.OnPatternAdded += EndpointData_OnPatternAdded;
            m_EndpointData.OnPatternRemoved += EndpointData_OnPatternRemoved;
            m_EndpointData.OnPatternChanged += EndpointData_OnPatternChanged;

            Restward.RestServer.RestServer thisRestServer = ProjectData.RestServerManager.RestServers.Where(c => c.RestServerData == data.Owner).FirstOrDefault();
            if (thisRestServer != null)
            {
                m_RestServerRunning = thisRestServer.IsServerRunning;
                if (m_RestServerRunning)
                {
                    SetRestServerEnables(false);
                }
            }
        }

        public EndpointData EndpointData
        {
            get
            {
                return m_EndpointData;
            }
        }

        public void SetRestServerEnables(bool enable)
        {
            txtAddress.Enabled = enable;
            comboBoxHttpMethod.Enabled = enable;
            comboBoxContentType.Enabled = enable;
            comboBoxStatusCode.Enabled = enable;
            checkBoxReponseDelay.Enabled = enable;
            txtResponseDelay.Enabled = enable;
            rtbResponse.Enabled = enable;
            toolStripButtonAdd.Enabled = enable;
            toolStripButtonRemovePattern.Enabled = enable;
        }

        private void UpdateSelectedPattern(PatternData data)
        {
            m_SelectedPattern = data;
            if (m_SelectedPattern != null)
            {
                SetReponseEnables(true);

                DataEnums.EnumWrapper<HttpMethod>[] HttpMethodDataSource = comboBoxHttpMethod.DataSource as DataEnums.EnumWrapper<HttpMethod>[];
                comboBoxHttpMethod.SelectedItem = HttpMethodDataSource.Where(c => c.Value == data.HttpMethod).First();

                DataEnums.EnumWrapper<HttpStatusCode>[] HttpStatusCodeDataSource = comboBoxStatusCode.DataSource as DataEnums.EnumWrapper<HttpStatusCode>[];
                comboBoxStatusCode.SelectedItem = HttpStatusCodeDataSource.Where(c => c.Value == data.ResponseData.ResponseCode).First();

                DataEnums.EnumWrapper<ContentType>[] ContentTypeDataSource = comboBoxContentType.DataSource as DataEnums.EnumWrapper<ContentType>[];
                comboBoxContentType.SelectedItem = ContentTypeDataSource.Where(c => c.Value == data.ResponseData.ContentType).First();

                checkBoxReponseDelay.Checked = data.ResponseData.UseResponseDelay;

                txtResponseDelay.Text = data.ResponseData.ResponseDelay.ToString();

                rtbResponse.Text = data.ResponseData.ResponseBody;
            }
            else
            {
                SetReponseEnables(false);
                toolStripButtonRemovePattern.Enabled = false;
            }
        }

        private void SetReponseEnables(bool enabled)
        {
            if (!m_RestServerRunning)
            {
                comboBoxHttpMethod.Enabled = enabled;
                comboBoxContentType.Enabled = enabled;
                comboBoxStatusCode.Enabled = enabled;
                checkBoxReponseDelay.Enabled = enabled;
                if (checkBoxReponseDelay.Checked || enabled == false)
                    txtResponseDelay.Enabled = enabled;

                rtbResponse.Enabled = enabled;
                toolStripButtonRemovePattern.Enabled = enabled;
            }
        }

        private void AddPattern(PatternData data)
        {
            ListViewItem newReponseItem = new ListViewItem(data.DetailedName);
            newReponseItem.Tag = data;

            listViewResponses.Items.Add(newReponseItem);

            newReponseItem.Text = data.DetailedName + "         )";
            newReponseItem.Text = data.DetailedName;

            UpdateSelectedPattern(data);
        }

        //Event Handlers
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            using (AddMockResponseDialog AMRD = new AddMockResponseDialog())
            {
                if (AMRD.ShowDialog() == DialogResult.OK)
                {
                    PatternData newData = new PatternData(AMRD.MockResponseName);
                    m_EndpointData.AddPattern(newData);
                }
            }
        }

        private void comboBoxHttpMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHttpMethod.SelectedItem != null)
            {
                DataEnums.EnumWrapper<HttpMethod> selectedItem = comboBoxHttpMethod.SelectedItem as DataEnums.EnumWrapper<HttpMethod>;
                if (selectedItem != null && m_SelectedPattern != null)
                {
                    m_SelectedPattern.HttpMethod = selectedItem.Value;
                } 
            }
        }

        private void comboBoxStatusCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStatusCode.SelectedItem != null)
            {
                DataEnums.EnumWrapper<HttpStatusCode> selectedItem = comboBoxStatusCode.SelectedItem as DataEnums.EnumWrapper<HttpStatusCode>;
                if (selectedItem != null && m_SelectedPattern != null)
                {
                    m_SelectedPattern.ResponseData.ResponseCode = selectedItem.Value;
                }
            }
        }

        private void comboBoxContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxContentType.SelectedItem != null)
            {
                DataEnums.EnumWrapper<ContentType> selectedItem = comboBoxContentType.SelectedItem as DataEnums.EnumWrapper<ContentType>;
                if (selectedItem != null && m_SelectedPattern != null)
                {
                    m_SelectedPattern.ResponseData.ContentType = selectedItem.Value;
                }
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            //validate url - todo
            m_EndpointData.HttpEndpointAddress = txtAddress.Text;
            lblListenerAddress.Text = m_EndpointData.Owner.ListenerAddress + m_EndpointData.HttpEndpointAddress;
        }

        private void checkBoxReponseDelay_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SelectedPattern != null)
            {
                if (checkBoxReponseDelay.Checked)
                {
                    m_SelectedPattern.ResponseData.UseResponseDelay = true;
                    txtResponseDelay.Enabled = true;
                }
                else
                {
                    txtResponseDelay.Enabled = false;
                }
            }
        }

        private void txtResponseDelay_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResponseDelay.Text) && m_SelectedPattern != null && txtResponseDelay.Text != m_SelectedPattern.ResponseData.ResponseDelay.ToString())
            {
                int tempDelay = m_SelectedPattern.ResponseData.ResponseDelay;
                int outDelay;
                if (!int.TryParse(txtResponseDelay.Text, out outDelay))
                {
                    txtResponseDelay.Text = tempDelay.ToString();
                    txtResponseDelay.SelectionStart = txtResponseDelay.Text.Length;
                    txtResponseDelay.SelectionLength = 0;
                }
                else
                {
                    m_SelectedPattern.ResponseData.ResponseDelay = outDelay;
                }
            }
        }

        private void rtbResponse_TextChanged(object sender, EventArgs e)
        {
            if (m_SelectedPattern != null && m_SelectedPattern.ResponseData.ResponseBody != rtbResponse.Text)
            {
                m_SelectedPattern.ResponseData.ResponseBody = rtbResponse.Text;
            }
        }

        private void ProjectData_OnRestServerChanged(object sender, OnRestServerChangedEventArgs e)
        {
            if (e.RestServer == m_EndpointData.Owner && e.PropertyName == "ListenerAddress")
            {
                lblListenerAddress.Text = e.RestServer.ListenerAddress + m_EndpointData.HttpEndpointAddress;
            }
        }

        private void RestServerManager_OnRestServerStopped(object sender, OnRestServerStoppedEventArgs e)
        {
            if (m_EndpointData.Owner == e.RestServer)
            {
                m_RestServerRunning = false;
                SetRestServerEnables(true);
            }
        }

        private void RestServerManager_OnRestServerStarted(object sender, OnRestServerStartedEventArgs e)
        {
            if (m_EndpointData.Owner == e.RestServer)
            {
                m_RestServerRunning = true;
                SetRestServerEnables(false);
            }
        }

        private void EndpointData_OnPatternAdded(object sender, OnPatternAddedEventArgs e)
        {
            AddPattern(e.Pattern);
        }

        private void EndpointData_OnPatternRemoved(object sender, OnPatternRemovedEventArgs e)
        {
        }

        private void EndpointData_OnPatternChanged(object sender, OnPatternChangedEventArgs e)
        {
            ListViewItem thisItem = listViewResponses.Items.Cast<ListViewItem>().Where(c => c.Tag == e.Pattern).FirstOrDefault();
            if (thisItem != null)
            {
                thisItem.Text = e.Pattern.DetailedName + "         )";
                thisItem.Text = e.Pattern.DetailedName;
            }
        }

        private void listViewResponses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewResponses.SelectedItems.Count > 1)
            {
                UpdateSelectedPattern(null);
            }
            else if (listViewResponses.SelectedItems.Count == 1)
            {
                ListViewItem thisItem = listViewResponses.SelectedItems[0];
                if (thisItem.Tag is PatternData)
                {
                    PatternData thisData = thisItem.Tag as PatternData;
                    UpdateSelectedPattern(thisData);
                }
            }
            else
            {
                UpdateSelectedPattern(null);
            }
        }

        private void toolStripButtonRemovePattern_Click(object sender, EventArgs e)
        {
            if (listViewResponses.SelectedItems.Count == 1)
            {
                ListViewItem thisItem = listViewResponses.SelectedItems[0];
                if (thisItem.Tag is PatternData)
                {
                    PatternData thisData = thisItem.Tag as PatternData;
                    m_EndpointData.RemovePattern(thisData);
                    thisItem.Remove();
                }
            }
        }
    }
}
