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
    public class MockServiceTabPage : TabPage
    {
        private MockRestServer m_MockRestServerUserControl;
        private RestServerData m_RestServerData;

        public MockServiceTabPage(RestServerData data) : base(data.Name)
        {
            m_RestServerData = data;
            m_MockRestServerUserControl = new MockRestServer(data);
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

        public RestServerData RestServerData
        {
            get
            {
                return m_RestServerData;
            }
        }
    }
}
