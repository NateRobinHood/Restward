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
    public partial class AddMockServiceDialog : Form
    {
        public AddMockServiceDialog()
        {
            InitializeComponent();
        }

        public string MockServiceName { get; private set; }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                return;
            }
            else
            {
                MockServiceName = txtName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
