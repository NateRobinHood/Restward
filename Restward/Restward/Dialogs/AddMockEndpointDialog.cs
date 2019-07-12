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
    public partial class AddMockEndpointDialog : Form
    {
        public AddMockEndpointDialog()
        {
            InitializeComponent();
        }
        public string MockEndpointName { get; private set; }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                return;
            }
            else
            {
                MockEndpointName = txtName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
