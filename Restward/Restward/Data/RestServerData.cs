using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restward.Data
{
    public class RestServerData : INotifyPropertyChanged
    {
        private int m_Port = 5500;
        private string m_BaseAddress = "intelligrated/api/v1/";
        private HttpProtocol m_Protocol = HttpProtocol.HTTP;
        private string m_Name = string.Empty;
        private bool m_UseAuth = false;
        private string m_AuthUser = string.Empty;
        private string m_AuthPassword = string.Empty;
        private string m_AuthToekn = string.Empty;
        private List<EndpointData> m_Endpoints = new List<EndpointData>();
        private bool m_IsDirty = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<OnEndpointAddedEventArgs> OnEndpointAdded;
        public event EventHandler<OnEndpointRemovedEventArgs> OnEndpointRemoved;
        public event EventHandler<OnEndpointChangedEventArgs> OnEndpointChanged;

        public RestServerData()
        {
        }

        public RestServerData(string name)
        {
            m_Name = name;
        }

        public RestServerData(JObject saveObject)
        {

        }

        private void UpdateAuthToken()
        {
            byte[] tokenBytes = Encoding.ASCII.GetBytes($"{m_AuthUser}:{m_AuthPassword}");
            AuthToken = $"Basic {Convert.ToBase64String(tokenBytes)}";
        }

        public int Port {
            get
            {
                return m_Port;
            }
            set
            {
                if (m_Port != value)
                {
                    m_Port = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ListenerAddress");
                }
            }
        }
        public string BaseAddress {
            get
            {
                return m_BaseAddress;
            }
            set
            {
                if (m_BaseAddress != value)
                {
                    m_BaseAddress = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ListenerAddress");
                }
            }
        }
        public HttpProtocol Protocol {
            get
            {
                return m_Protocol;
            }
            set
            {
                if (m_Protocol != value)
                {
                    m_Protocol = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ListenerAddress");
                }
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
        public bool UseAuth
        {
            get
            {
                return m_UseAuth;
            }
            set
            {
                if (m_UseAuth != value)
                {
                    m_UseAuth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string AuthUser
        {
            get
            {
                return m_AuthUser;
            }
            set
            {
                if (m_AuthUser != value)
                {
                    m_AuthUser = value;
                    NotifyPropertyChanged();
                    UpdateAuthToken();
                }
            }
        }
        public string AuthPassword
        {
            get
            {
                return m_AuthPassword;
            }
            set
            {
                if (m_AuthPassword != value)
                {
                    m_AuthPassword = value;
                    NotifyPropertyChanged();
                    UpdateAuthToken();
                }
            }
        }
        [JsonIgnore]
        public string AuthToken
        {
            get
            {
                return m_AuthToekn;
            }
            private set
            {
                if (m_AuthToekn != value)
                {
                    m_AuthToekn = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [JsonIgnore]
        public string ListenerAddress
        {
            get
            {
                string protocol = "http://localhost";
                switch (Protocol)
                {
                    case HttpProtocol.HTTP:
                        protocol =  "http://localhost";
                        break;
                    case HttpProtocol.HTTPS:
                        protocol = "https://localhost";
                        break;
                }

                if (m_BaseAddress.EndsWith("/") || m_BaseAddress.EndsWith("\\"))
                {
                    return $"{protocol}:{m_Port}/{m_BaseAddress}";
                }
                else
                {
                    return $"{protocol}:{m_Port}/{m_BaseAddress}/";
                }
            }
        }
        public IEnumerable<EndpointData> Endpoints
        {
            get
            {
                return m_Endpoints;
            }
        }

        public bool IsDirty
        {
            get
            {
                return m_IsDirty || m_Endpoints.Any(c => c.IsDirty);
            }
        }

        public void AddEndpoint(EndpointData data)
        {
            data.PropertyChanged += Endpoint_PropertyChanged;

            m_Endpoints.Add(data);
            data.Owner = this;

            OnEndpointAdded?.Invoke(this, new OnEndpointAddedEventArgs(this, data));

            m_IsDirty = true;
        }

        public void RemoveEndpoint(EndpointData data)
        {
            if (m_Endpoints.Contains(data))
            {
                data.PropertyChanged -= Endpoint_PropertyChanged;

                m_Endpoints.Remove(data);
                data.Owner = null;

                OnEndpointRemoved?.Invoke(this, new OnEndpointRemovedEventArgs(this, data));

                m_IsDirty = true;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return (userName.ToLower() == m_AuthUser && password == m_AuthPassword);
        }

        public void FinalizeDeserializtion()
        {
            foreach (EndpointData data in m_Endpoints)
            {
                data.PropertyChanged += Endpoint_PropertyChanged;
                data.Owner = this;
                data.FinalizeDeserializtion();
            }
        }

        public void ResetIsDirty()
        {
            m_IsDirty = false;
            m_Endpoints.ForEach(c => c.ResetIsDirty());
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Endpoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is EndpointData)
            {
                OnEndpointChanged?.Invoke(sender, new OnEndpointChangedEventArgs(this, sender as EndpointData, e));
            }

            m_IsDirty = true;
        }
    }
}
