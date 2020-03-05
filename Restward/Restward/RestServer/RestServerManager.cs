using Restward.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restward.RestServer
{
    /// <summary>
    /// Manages the rest servers
    /// </summary>
    public class RestServerManager
    {
        private List<RestServer> m_RestServerList = new List<RestServer>();
        //private bool m_IsThreadTerminationRequested = false;

        public event EventHandler<OnRestServerTraceEventArgs> OnRestServerTrace;
        public event EventHandler<OnRestServerStartedEventArgs> OnRestServerStarted;
        public event EventHandler<OnRestServerStoppedEventArgs> OnRestServerStopped;

        public RestServerManager()
        {
        }

        private RestServer CreateRestServer(RestServerData data)
        {
            try
            {
                RestServer newRestServer = new RestServer(data);

                newRestServer.OnRestServerTrace += RestServer_OnRestServerTrace;
                newRestServer.OnRestServerStarted += RestServer_OnRestServerStarted;
                newRestServer.OnRestServerStopped += RestServer_OnRestServerStopped;
                m_RestServerList.Add(newRestServer);

                return newRestServer;
            }
            catch (Exception ex)
            {
                TraceWriteLine(ex.ToString());
            }

            return null;
        }

        public void StopServer(RestServerData data)
        {
            RestServer targetRestServer = m_RestServerList.Where(c => c.RestServerData == data).FirstOrDefault();
            if (targetRestServer != null)
            {
                targetRestServer.Close();
                m_RestServerList.Remove(targetRestServer);

                //OnRestServerStopped?.Invoke(this, new OnRestServerStoppedEventArgs(data));
            }
        }

        public void StartServer(RestServerData data)
        {
            RestServer targetRestServer = m_RestServerList.Where(c => c.RestServerData == data).FirstOrDefault();
            if (targetRestServer != null)
            {
                try
                {
                    TraceWriteLine($"Rest Server {targetRestServer.Name} initializing run routine");
                    targetRestServer.Run();
                    TraceWriteLine($"Rest Server {targetRestServer.Name} leaving run routine");

                    TraceWriteLine($"Rest server {data.Name} running");

                    //OnRestServerStarted?.Invoke(this, new OnRestServerStartedEventArgs(data));
                }
                catch (Exception ex)
                {
                    TraceWriteLine(ex.ToString());
                }
            }
            else
            {
                targetRestServer = CreateRestServer(data);
                try
                {
                    TraceWriteLine($"Rest Server {targetRestServer.Name} initializing run routine");
                    targetRestServer.Run();
                    TraceWriteLine($"Rest Server {targetRestServer.Name} leaving run routine");

                    TraceWriteLine($"Rest server {data.Name} running");

                    //OnRestServerStarted?.Invoke(this, new OnRestServerStartedEventArgs(data));
                }
                catch (Exception ex)
                {
                    TraceWriteLine(ex.ToString());
                }
            }
        }

        public IEnumerable<RestServer> RestServers
        {
            get
            {
                return m_RestServerList;
            }
        }

        /// <summary>
        /// Service function that will run the rest servers
        /// </summary>
        //protected void ServiceThreadFunction()
        //{
        //    while (!m_IsThreadTerminationRequested)
        //    {
        //        foreach (RestServer server in m_RestServerList)
        //        {
        //            if (!server.IsServerRunning)
        //            {
        //                try
        //                {
        //                    TraceWriteLine($"Rest Server {server.Name} initializing run routine");
        //                    server.Run();
        //                    TraceWriteLine($"Rest Server {server.Name} leaving run routine");
        //                }
        //                catch (Exception ex)
        //                {
        //                    TraceWriteLine(ex.ToString());
        //                }
        //            }
        //        }

        //        Thread.Sleep(100);
        //    }
        //}

        private void TraceWriteLine(string message)
        {
            OnRestServerTrace?.Invoke(this, new OnRestServerTraceEventArgs(null, message));
        }

        private void RestServer_OnRestServerTrace(object sender, OnRestServerTraceEventArgs e)
        {
            OnRestServerTrace?.Invoke(this, e);
        }

        private void RestServer_OnRestServerStopped(object sender, OnRestServerStoppedEventArgs e)
        {
            OnRestServerStopped?.Invoke(this, e);
        }

        private void RestServer_OnRestServerStarted(object sender, OnRestServerStartedEventArgs e)
        {
            OnRestServerStarted?.Invoke(this, e);
        }
    }
}
