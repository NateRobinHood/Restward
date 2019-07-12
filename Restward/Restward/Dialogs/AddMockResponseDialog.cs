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
    public partial class AddMockResponseDialog : Form
    {
        public AddMockResponseDialog()
        {
            InitializeComponent();
        }
        public string MockResponseName { get; private set; }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                return;
            }
            else
            {
                MockResponseName = txtName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
