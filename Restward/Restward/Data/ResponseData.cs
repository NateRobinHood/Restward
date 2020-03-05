using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Restward.Data
{
    public class ResponseData : INotifyPropertyChanged
    {
        private HttpStatusCode m_ResponseCode = HttpStatusCode.OK;
        private ContentType m_ContentType = ContentType.json;
        private bool m_UseResponseDelay = false;
        private int m_ResponseDelay = 100;
        private string m_ResponseBody = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public ResponseData()
        {
        }

        public HttpStatusCode ResponseCode
        {
            get
            {
                return m_ResponseCode;
            }
            set
            {
                if (m_ResponseCode != value)
                {
                    m_ResponseCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ContentType ContentType
        {
            get
            {
                return m_ContentType;
            }
            set
            {
                if (m_ContentType != value)
                {
                    m_ContentType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool UseResponseDelay
        {
            get
            {
                return m_UseResponseDelay;
            }
            set
            {
                if (m_UseResponseDelay != value)
                {
                    m_UseResponseDelay = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int ResponseDelay
        {
            get
            {
                return m_ResponseDelay;
            }
            set
            {
                if (m_ResponseDelay != value)
                {
                    m_ResponseDelay = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ResponseBody
        {
            get
            {
                return m_ResponseBody;
            }
            set
            {
                if (m_ResponseBody != value)
                {
                    m_ResponseBody = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GetResponse(JObject payload)
        {
            if (m_UseResponseDelay)
                Thread.Sleep(m_ResponseDelay);

            return m_ResponseBody;
        }

        public string GetResponse(XDocument payload)
        {
            if (m_UseResponseDelay)
                Thread.Sleep(m_ResponseDelay);

            return m_ResponseBody;
        }

        public string GetResponse()
        {
            return m_ResponseBody;
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
