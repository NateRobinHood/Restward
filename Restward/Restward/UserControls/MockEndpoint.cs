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

namespace Restward.UserControls
{
    public partial class MockEndpoint : UserControl
    {
        public MockEndpoint()
        {
            InitializeComponent();

            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(Color.FromArgb(195, 195, 195), Color.FromArgb(110, 110, 110));
            GradientRenderer.RoundedEdges = false;

            toolStripMain.Renderer = GradientRenderer;
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            using (AddMockResponseDialog AMRD = new AddMockResponseDialog())
            {
                if (AMRD.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem newReponseItem = new ListViewItem(AMRD.MockResponseName);
                }
            }
        }
    }
}
