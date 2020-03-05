using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restward.Data
{
    public class PatternData : INotifyPropertyChanged
    {
        private HttpMethod m_HttpMethod = HttpMethod.Get;
        private ResponseData m_ResponseData = new ResponseData();
        private string m_Name = string.Empty;
        private bool m_IsDirty = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public PatternData(string name)
        {
            m_Name = name;

            m_ResponseData.PropertyChanged += ResponseData_PropertyChanged;
        }

        public HttpMethod HttpMethod
        {
            get
            {
                return m_HttpMethod;
            }
            set
            {
                if (m_HttpMethod != value)
                {
                    m_HttpMethod = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public ResponseData ResponseData
        {
            get
            {
                return m_ResponseData;
            }
            set
            {
                m_ResponseData = value;
            }
        }

        [JsonIgnore]
        public string DetailedName
        {
            get
            {
                return $"{m_Name}({m_HttpMethod.GetDescriptorText()} {m_ResponseData.ResponseCode.GetDescriptorText()} {m_ResponseData.ContentType.GetDescriptorText()})";
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsDirty
        {
            get
            {
                return m_IsDirty;
            }
        }

        public void ResetIsDirty()
        {
            m_IsDirty = false;
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResponseData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ResponseCode" || e.PropertyName == "ContentType")
                NotifyPropertyChanged("Name");

            m_IsDirty = true;
        }
    }
}
