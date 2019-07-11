using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restward.UserControls
{
    public partial class MockRestServer : UserControl
    {
        public MockRestServer()
        {
            InitializeComponent();

            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(Color.FromArgb(195, 195, 195), Color.FromArgb(110, 110, 110));
            GradientRenderer.RoundedEdges = false;

            toolStripMain.Renderer = GradientRenderer;
        }
    }
}
