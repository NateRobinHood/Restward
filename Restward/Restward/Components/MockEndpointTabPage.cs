using Restward.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restward.Components
{
    public class MockEndpointTabPage : TabPage
    {
        private MockEndpoint m_MockEndpointUserControl;

        public MockEndpointTabPage() : base()
        {
            m_MockEndpointUserControl = new MockEndpoint();
            this.Controls.Add(m_MockEndpointUserControl);
            m_MockEndpointUserControl.Dock = DockStyle.Fill;
        }

        public MockEndpointTabPage(string name) : base(name)
        {
            m_MockEndpointUserControl = new MockEndpoint();
            this.Controls.Add(m_MockEndpointUserControl);
            m_MockEndpointUserControl.Dock = DockStyle.Fill;
        }

        public MockEndpoint MockEndpointUserControl
        {
            get
            {
                return m_MockEndpointUserControl;
            }
        }
    }
}
