using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restward.Data
{
    public class OnRestServerAddedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }

        public OnRestServerAddedEventArgs(RestServerData restServer)
        {
            RestServer = restServer;
        }
    }

    public class OnRestServerRemovedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }

        public OnRestServerRemovedEventArgs(RestServerData restServer)
        {
            RestServer = restServer;
        }
    }

    public class OnRestServerChangedEventArgs : PropertyChangedEventArgs
    {
        public RestServerData RestServer { get; private set; }
        //public PropertyChangedEventArgs PropertyChanged { get; private set; }

        public OnRestServerChangedEventArgs(RestServerData restServer, PropertyChangedEventArgs propertyChanged) : base(propertyChanged.PropertyName)
        {
            RestServer = restServer;
            //PropertyChanged = propertyChanged;
        }
    }

    public class OnEndpointAddedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }
        public EndpointData Endpoint { get; private set; }

        public OnEndpointAddedEventArgs(RestServerData restServer, EndpointData endpoint)
        {
            RestServer = restServer;
            Endpoint = endpoint;
        }
    }

    public class OnEndpointRemovedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }
        public EndpointData Endpoint { get; private set; }

        public OnEndpointRemovedEventArgs(RestServerData restServer, EndpointData endpoint)
        {
            RestServer = restServer;
            Endpoint = endpoint;
        }
    }

    public class OnEndpointChangedEventArgs : PropertyChangedEventArgs
    {
        public RestServerData RestServer { get; private set; }
        public EndpointData Endpoint { get; private set; }
        //public PropertyChangedEventArgs PropertyChanged { get; private set; }

        public OnEndpointChangedEventArgs(RestServerData restServer, EndpointData endpoint, PropertyChangedEventArgs propertyChanged) : base(propertyChanged.PropertyName)
        {
            RestServer = restServer;
            Endpoint = endpoint;
            //PropertyChanged = propertyChanged;
        }
    }

    public class OnPatternAddedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }
        public EndpointData Endpoint { get; private set; }
        public PatternData Pattern { get; private set; }

        public OnPatternAddedEventArgs(RestServerData restServer, EndpointData endpoint, PatternData pattern)
        {
            RestServer = restServer;
            Endpoint = endpoint;
            Pattern = pattern;
        }
    }

    public class OnPatternRemovedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }
        public EndpointData Endpoint { get; private set; }
        public PatternData Pattern { get; private set; }

        public OnPatternRemovedEventArgs(RestServerData restServer, EndpointData endpoint, PatternData pattern)
        {
            RestServer = restServer;
            Endpoint = endpoint;
            Pattern = pattern;
        }
    }

    public class OnPatternChangedEventArgs : PropertyChangedEventArgs
    {
        public RestServerData RestServer { get; private set; }
        public EndpointData Endpoint { get; private set; }
        public PatternData Pattern { get; private set; }
        //public PropertyChangedEventArgs PropertyChanged { get; private set; }

        public OnPatternChangedEventArgs(RestServerData restServer, EndpointData endpoint, PatternData pattern, PropertyChangedEventArgs propertyChanged) : base(propertyChanged.PropertyName)
        {
            RestServer = restServer;
            Endpoint = endpoint;
            Pattern = pattern;
            //PropertyChanged = propertyChanged;
        }
    }

    public class OnRestServerTraceEventArgs : EventArgs
    {
        public RestServerData RestServerData { get; private set; }
        public string Message { get; private set; }

        public OnRestServerTraceEventArgs(RestServerData data, string message)
        {
            RestServerData = data;
            Message = $"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}] {message}";
        }
    }

    public class OnRestServerStartedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }

        public OnRestServerStartedEventArgs(RestServerData restServer)
        {
            RestServer = restServer;
        }
    }

    public class OnRestServerStoppedEventArgs : EventArgs
    {
        public RestServerData RestServer { get; private set; }

        public OnRestServerStoppedEventArgs(RestServerData restServer)
        {
            RestServer = restServer;
        }
    }
}
