using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restward.RestServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restward.Data
{
    public class ProjectData
    {
        public static event EventHandler<OnRestServerAddedEventArgs> OnRestServerAdded;
        public static event EventHandler<OnRestServerRemovedEventArgs> OnRestServerRemoved;
        public static event EventHandler<OnRestServerChangedEventArgs> OnRestServerChanged;

        private static List<RestServerData> m_RestServers = null;
        private static RestServerManager m_RestServerManager = null;
        private static bool m_IsDirty = false;

        public static IEnumerable<RestServerData> RestServers
        {
            get
            {
                if (m_RestServers == null)
                    m_RestServers = new List<RestServerData>();

                return m_RestServers;
            }
        }

        public static bool IsDirty
        {
            get
            {
                return m_IsDirty || m_RestServers.Any(c => c.IsDirty);
            }
        }

        public static RestServerManager RestServerManager
        {
            get
            {
                if (m_RestServerManager == null)
                    m_RestServerManager = new RestServerManager();

                return m_RestServerManager;
            }
        }

        public static void AddRestServer(RestServerData data)
        {
            if (m_RestServers == null)
                m_RestServers = new List<RestServerData>();

            data.PropertyChanged += RestServer_PropertyChanged;

            m_RestServers.Add(data);

            OnRestServerAdded?.Invoke(RestServers, new OnRestServerAddedEventArgs(data));

            m_IsDirty = true;
        }

        public static void RemoveRestServer(RestServerData data)
        {
            if (m_RestServers == null)
                m_RestServers = new List<RestServerData>();

            if (m_RestServers.Contains(data))
            {
                data.PropertyChanged -= RestServer_PropertyChanged;

                m_RestServers.Remove(data);

                OnRestServerRemoved?.Invoke(RestServers, new OnRestServerRemovedEventArgs(data));
            }

            m_IsDirty = true;
        }

        public static string GetSaveFile()
        {
            string saveFileContent = JsonConvert.SerializeObject(m_RestServers);
            return saveFileContent;// $"{{\"RestServers\": {saveFileContent}}}";
        }

        public static void LoadSaveFile(string fileContents)
        {
            //JObject saveFile = JObject.Parse(fileContents);
            //JArray restServers = saveFile["RestServers"] as JArray;
            JArray saveFile = JArray.Parse(fileContents);
            foreach (JObject obj in saveFile)
            {
                RestServerData thisRestServerData = JsonConvert.DeserializeObject<RestServerData>(obj.ToString());
                thisRestServerData.FinalizeDeserializtion();
                AddRestServer(thisRestServerData);
            }
        }

        public static void ResetIsDirty()
        {
            m_IsDirty = false;
            m_RestServers.ForEach(c => c.ResetIsDirty());
        }

        private static void RestServer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(sender is RestServerData)
            {
                OnRestServerChanged?.Invoke(sender, new OnRestServerChangedEventArgs(sender as RestServerData, e));
            }

            m_IsDirty = true;
        }
    }
}
