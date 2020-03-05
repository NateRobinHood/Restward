using Restward.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restward.Dialogs
{
    public partial class LogDialog : Form
    {
        private RestServerData m_RestServerData;

        public LogDialog(RestServerData data)
        {
            InitializeComponent();

            m_RestServerData = data;

            this.Text = $"Restward - {data.Name} Log View";

            ProjectData.RestServerManager.OnRestServerTrace += RestServerManager_OnRestServerTrace;
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

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }
    }
}
