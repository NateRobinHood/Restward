using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restward.Data
{
    public class RestServerDataException : Exception
    {
        private RestServerData m_RestServerData;
        private string m_Property = string.Empty;
        private string m_Message = string.Empty;

        public RestServerDataException(RestServerData data, string prop, string message) : base(message)
        {
            m_RestServerData = data;
            m_Property = prop;
            m_Message = message;
        }

        public string UserMessage
        {
            get
            {
                return $"[RestServer({m_RestServerData.Name}):{m_Property}] {Message}";
            }
        }
    }

    public class EndpointDataException : Exception
    {
        private EndpointData m_EndpointData;
        private string m_Property = string.Empty;
        private string m_Message = string.Empty;

        public EndpointDataException(EndpointData endpointData, string prop, string message) : base(message)
        {
            m_EndpointData = endpointData;
            m_Property = prop;
            m_Message = message;
        }

        public string UserMessage
        {
            get
            {
                return $"[Endpoint({m_EndpointData.Name}):{m_Property}] {Message}";
            }
        }
    }
}
