using Restward.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restward.Components
{
    public class MockServiceTabPage : TabPage
    {
        private MockRestServer m_MockRestServerUserControl;

        public MockServiceTabPage() : base()
        {
            m_MockRestServerUserControl = new MockRestServer();
            this.Controls.Add(m_MockRestServerUserControl);
            m_MockRestServerUserControl.Dock = DockStyle.Fill;
        }

        public MockServiceTabPage(string name) : base(name)
        {
            m_MockRestServerUserControl = new MockRestServer();
            this.Controls.Add(m_MockRestServerUserControl);
            m_MockRestServerUserControl.Dock = DockStyle.Fill;
        }

        public MockRestServer MockRestServerUserControl
        {
            get
            {
                return m_MockRestServerUserControl;
            }
        }
    }
}
