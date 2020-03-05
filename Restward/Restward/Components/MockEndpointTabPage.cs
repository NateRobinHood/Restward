using Restward.Data;
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
        private EndpointData m_EndpointData;

        public MockEndpointTabPage(EndpointData data) : base(data.Name)
        {
            m_EndpointData = data;
            m_MockEndpointUserControl = new MockEndpoint(data);
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

        public EndpointData EndpointData
        {
            get
            {
                return m_EndpointData;
            }
        }
    }
}
