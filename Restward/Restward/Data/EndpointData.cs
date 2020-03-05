using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restward.Data
{
    public class EndpointData : INotifyPropertyChanged
    {
        private string m_HttpEndpointAddress = string.Empty;
        private string m_Name = string.Empty;
        private RestServerData m_Owner = null;
        private List<PatternData> m_Patterns = new List<PatternData>();
        private bool m_IsDirty = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<OnPatternAddedEventArgs> OnPatternAdded;
        public event EventHandler<OnPatternRemovedEventArgs> OnPatternRemoved;
        public event EventHandler<OnPatternChangedEventArgs> OnPatternChanged;

        public EndpointData()
        {

        }

        public EndpointData(string name)
        {
            m_Name = name;
        }

        public string HttpEndpointAddress
        {
            get
            {
                return m_HttpEndpointAddress;
            }
            set
            {
                if (m_HttpEndpointAddress != value)
                {
                    m_HttpEndpointAddress = value;
                    NotifyPropertyChanged();
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

        [Browsable(false)]
        [JsonIgnore]
        public RestServerData Owner
        {
            get
            {
                return m_Owner;
            }
            set
            {
                if (m_Owner != value)
                {
                    m_Owner = value;
                }
            }
        }

        public IEnumerable<PatternData> Patterns
        {
            get
            {
                return m_Patterns;
            }
        }

        public bool IsDirty
        {
            get
            {
                return m_IsDirty || m_Patterns.Any(c => c.IsDirty);
            }
        }

        public void AddPattern(PatternData data)
        {
            data.PropertyChanged += Pattern_PropertyChanged;

            m_Patterns.Add(data);

            OnPatternAdded?.Invoke(this, new OnPatternAddedEventArgs(m_Owner, this, data));

            m_IsDirty = true;
        }

        public void RemovePattern(PatternData data)
        {
            if (m_Patterns.Contains(data))
            {
                data.PropertyChanged -= Pattern_PropertyChanged;

                m_Patterns.Remove(data);

                OnPatternRemoved?.Invoke(this, new OnPatternRemovedEventArgs(m_Owner, this, data));

                m_IsDirty = true;
            }
        }

        public void FinalizeDeserializtion()
        {
            foreach (PatternData data in m_Patterns)
            {
                data.PropertyChanged += Pattern_PropertyChanged;
            }
        }

        public void ResetIsDirty()
        {
            m_IsDirty = false;
            m_Patterns.ForEach(c => c.ResetIsDirty());
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Pattern_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is PatternData)
            {
                OnPatternChanged?.Invoke(this, new OnPatternChangedEventArgs(m_Owner, this, sender as PatternData, e));
            }

            m_IsDirty = true;
        }
    }
}
