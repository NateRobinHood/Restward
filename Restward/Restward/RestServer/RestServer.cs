using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restward.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Restward.RestServer
{
    /// <summary>
    /// A Rest Server for hosting http and https endpoints
    /// </summary>
    public class RestServer
    {
        /// <summary>
        /// Constant for json mime type
        /// </summary>
        public const string MIME_TYPE_JSON = "application/json";
        /// <summary>
        /// Constant for xml mime type
        /// </summary>
        public const string MIME_TYPE_XML = "application/xml";
        /// <summary>
        /// Raw plain text mime type
        /// </summary>
        public const string MIME_TYPE_PLAIN = "text/plain";

        private readonly HttpListener listener = new HttpListener();
        private int m_ConcurrentConnections = 4;
        //private int m_EndpointTimeout = -1;
        private Semaphore sem;
        private bool m_IsServerRunning = false;
        private bool m_IsCloseRequested = false;
        private bool m_UseBasicAuth = false;
        private int m_ActiveRequests = 0;

        private string m_BaseAddress = string.Empty;
        private string m_PrefixAddress = string.Empty;
        private string m_ServerName = string.Empty;
        private List<RestEndpoint> m_Endpoints = new List<RestEndpoint>();
        private RestServerData m_RestServerData;

        public event EventHandler<OnRestServerTraceEventArgs> OnRestServerTrace;
        public event EventHandler<OnRestServerStoppedEventArgs> OnRestServerStopped;
        public event EventHandler<OnRestServerStartedEventArgs> OnRestServerStarted;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commManager"></param>
        /// <param name="xmlConfig"></param>
        /// <param name="accepts"></param>
        public RestServer(RestServerData data, int accepts = 4)
        {
            m_RestServerData = data;

            m_BaseAddress = data.ListenerAddress;
            if (m_BaseAddress == string.Empty)
            {
                throw new RestServerDataException(data,
                        "BaseAddress",
                        "base address is required");
            }
            else
            {
                //adjust base address to replace ip address with *
                //this is needed to accept external requests
                Uri baseAddressUri = new Uri(m_BaseAddress);

                //validate base address
                if (baseAddressUri.Scheme != "http" && baseAddressUri.Scheme != "https")
                {
                    throw new RestServerDataException(data,
                        "BaseAddress",
                        "base address requires a defined URI scheme, either http or https");
                }
                else if (baseAddressUri.IsDefaultPort)
                {
                    throw new RestServerDataException(data,
                        "BaseAddress",
                        string.Format("base address requires a defined port other than the default of {0}", baseAddressUri.Port));
                }

                m_PrefixAddress = string.Format("{0}://*:{1}{2}", baseAddressUri.Scheme, baseAddressUri.Port, baseAddressUri.AbsolutePath);
            }

            m_ServerName = data.Name;
            if (m_ServerName == string.Empty)
            {
                throw new RestServerDataException(data,
                        "Name",
                        "name is a required attribute");
            }

            m_ConcurrentConnections = 4;//xmlConfig["concurrent_connections", 4];

            //m_EndpointTimeout = 5000;//xmlConfig["timeout", 5000];

            if (data.UseAuth)
                m_UseBasicAuth = true;

            foreach (EndpointData endpoint in data.Endpoints)
            {
                foreach (PatternData pattern in endpoint.Patterns)
                {
                    try
                    {
                        m_Endpoints.Add(new RestEndpoint(endpoint, pattern, m_BaseAddress));
                    }
                    catch (Exception ex)
                    {
                        throw new RestServerDataException(data, "Endpoint", $"Endpoint with address {endpoint.HttpEndpointAddress} failed to initialize.\n{ex.ToString()}");
                    }
                }
            }

            listener.IgnoreWriteExceptions = true;

            // Multiply by number of cores:
            this.m_ConcurrentConnections = accepts * Environment.ProcessorCount;
        }

        /// <summary>
        /// Boolean that is true when the rest server is running
        /// </summary>
        public bool IsServerRunning
        {
            get
            {
                return m_IsServerRunning;
            }
        }

        /// <summary>
        /// The name of the server
        /// </summary>
        public string Name
        {
            get
            {
                return m_ServerName;
            }
        }

        public RestServerData RestServerData
        {
            get
            {
                return m_RestServerData;
            }
        }

        /// <summary>
        /// Will release resources and close the rest server
        /// </summary>
        public void Close()
        {
            m_IsCloseRequested = true;
            sem.Close();
            listener.Stop();

            int wiatCount = 0;
            int waitTimeout = 30;
            while (m_ActiveRequests > 0 && wiatCount < waitTimeout)
            {
                TraceWriteLine($"[RestServer:Close] Waiting to shutdown rest server {m_ServerName}, {m_ActiveRequests} active requests");
                Thread.Sleep(1000);
            }

            if (wiatCount == waitTimeout)
            {
                TraceWriteLine($"[RestServer: Close] Waited {waitTimeout} seconds, shutting down anyways with {m_ActiveRequests} active requests");
            }

            listener.Prefixes.Clear();
            listener.Close();
        }

        /// <summary>
        /// Will run the rest server
        /// </summary>
        public async void Run()
        {
            //Intialize endpoint rest jobs
            //foreach (RestEndpoint endpoint in m_Endpoints)
            //{
            //    endpoint.InitializeRestJob(m_CommManager);
            //}

            List<string> uriPrefixes = new List<string>();
            uriPrefixes.Add(m_PrefixAddress);
            uriPrefixes.AddRange(m_Endpoints.Select(c =>
            {
                if (c.IsQueryAddress)
                {
                    return m_PrefixAddress + c.PreQueryAddress;
                }
                else if (c.IsTemplateAddress)
                {
                    return m_PrefixAddress + c.PreTemplateAddress;
                }
                else
                {
                    return m_PrefixAddress + c.Address;
                }
            }));

            //insure the uri prefixes have ending slashes
            for(int i = 0; i < uriPrefixes.Count; i++)
            {
                if (uriPrefixes[i].Last() != '/' && uriPrefixes[i].Last() != '\\')
                {
                    uriPrefixes[i] += "/";
                }
            }

            m_IsServerRunning = true;

            TraceWriteLine($"Rest Server {m_ServerName} adding listener bindings");
            // Add the server bindings:
            foreach (var prefix in uriPrefixes)
            {
                TraceWriteLine($"Rest Server {m_ServerName} adding listener binding for prefix {prefix}");
                listener.Prefixes.Add(prefix);
            }

            if(m_UseBasicAuth)
                listener.AuthenticationSchemes = AuthenticationSchemes.Basic;

            TraceWriteLine($"Rest Server {m_ServerName} initializing listener");
            bool retryStart = false;
            int retryCount = 0;
            int maxRetry = 15;
            do
            {
                try
                {
                    listener.Start();
                    retryStart = false;
                }
                catch (HttpListenerException ex)
                {
                    TraceWriteLine($"Rest Server {m_ServerName} failed to start with exception {ex.Message}");
                    if (ex.Message == "Access is denied")
                    {
                        TraceWriteLine($"Rest Server {m_ServerName} failed to start due to lack of permissions, restart the application as Administrator.");
                        retryStart = false;
                        OnRestServerStopped?.Invoke(this, new OnRestServerStoppedEventArgs(m_RestServerData));
                        return;
                    }
                    else if (retryCount < maxRetry)
                    {                       
                        retryStart = true;
                        retryCount++;
                        TraceWriteLine($"Retrying Rest Server {m_ServerName} start, retry count {retryCount}");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        TraceWriteLine($"Retried Rest Server {m_ServerName} start {retryCount} times, throwing failed to start exception");
                        retryStart = false;
                        //throw new RestServerDataException(m_RestServerData, "", $"Rest Server {m_ServerName} failed to start after {retryCount} retries.\n{ex.ToString()}");
                        OnRestServerStopped?.Invoke(this, new OnRestServerStoppedEventArgs(m_RestServerData));
                        return;
                    }
                }
            } while (retryStart);
            TraceWriteLine($"Rest Server {m_ServerName} initialized listener");

            //Accept connections:
            //1. Higher values mean more connections can be maintained yet at a 
            //   much slower average response time; fewer connections will be rejected.
            //2. Lower values mean less connections can be maintained yet at a 
            //   much faster average response time; more connections will be rejected.
            sem = new Semaphore(m_ConcurrentConnections, m_ConcurrentConnections);
            try
            {
                await RunServer();
            }
            catch (Exception ex)
            {
                TraceWriteLine(ex.ToString());
            }
            finally
            {
                m_IsServerRunning = false;
                if(!m_IsCloseRequested)
                    listener.Stop();
            }

            OnRestServerStopped?.Invoke(this, new OnRestServerStoppedEventArgs(m_RestServerData));
        }


        private async Task RunServer()
        {
            while (!m_IsCloseRequested)
            {
                // Fall through until we've initialized all our connection listeners.
                // When the semaphore blocks (its count reaches 0) we wait until a connection occurs, 
                // upon which the semaphore is released and we create another connection "awaiter."
                sem.WaitOne();

                if (m_IsCloseRequested)
                    return;

                await StartConnectionListener();
            }
        }

        private async Task StartConnectionListener()
        {
            // Wait for a connection
            HttpListenerContext context = null;
            try
            {
                OnRestServerStarted?.Invoke(this, new OnRestServerStartedEventArgs(m_RestServerData));
                context = await listener.GetContextAsync();
            }
            catch (ObjectDisposedException)
            {
                //closing
                if (m_IsCloseRequested)
                    return;

                if (context == null)
                {
                    TraceWriteLine($"Rest Server {m_ServerName}, Failed to get http context from request");
                    return;
                }
            }

            // Allow a new connection listener to be set up.
            int semCount = sem.Release();

            //Trace.WriteLine($"Rest Server {m_ServerName} listener connection with semaphore instance count at {semCount}");

            //process the current request
            Thread processThread = new Thread(() => 
            {
                // m_ActiveRequests will keep track of currently active requests for the purposes of shuting down cleanly
                m_ActiveRequests++;
                try
                {
                    ProcessRequest(context);
                }
                catch(Exception ex)
                {
                    TraceWriteLine(ex.ToString());
                }
                finally
                {
                    m_ActiveRequests--;
                }
            });
            processThread.Start();
        }


        private void ProcessRequest(HttpListenerContext context)
        {
            // Setup Chain of Responsibility for route processing
            Uri requestUri = context.Request.Url;

            TraceWriteLine($"Rest Server {m_ServerName} processing {context.Request.HttpMethod} request for uri {requestUri.AbsoluteUri}");

            //Check auth
            if (m_UseBasicAuth)
            {
                //if (CommManager.ValidateICWUser == null)
                //{
                //    context.Response.StatusCode = 500;
                //    Trace.WriteLine(string.Format("Rest Server {0} received request with credentials, the server was unable to validate credentials. ValidateICWUser was null", m_ServerName));
                //    context.Response.OutputStream.Close();
                //    return;
                //}

                HttpListenerBasicIdentity identity = (HttpListenerBasicIdentity)context.User.Identity;
                if (identity == null || m_RestServerData.ValidateUser(identity.Name, identity.Password) == false)
                {
                    context.Response.StatusCode = 401;
                    TraceWriteLine(string.Format("Rest Server {0} received request that had invalid credentials", m_ServerName));
                    context.Response.OutputStream.Close();
                    return;
                }
            }

            List<RestEndpoint> macthedEndpoints = m_Endpoints.Where(c => c.RequestMatch(context.Request)).ToList();
            RestEndpoint restEndpoint = macthedEndpoints.FirstOrDefault();
            if (macthedEndpoints.Count > 1)
            {
                List<Tuple<int, RestEndpoint>> matchesWithCounts = macthedEndpoints.Select(c => new Tuple<int, RestEndpoint>(c.TemplateMatchCount(context.Request), c)).ToList();
                Tuple<int, RestEndpoint> targetMatch = matchesWithCounts.Aggregate((c1, c2) => c1.Item1 < c2.Item1 ? c1 : c2);
                restEndpoint = targetMatch.Item2;
                if (matchesWithCounts.Count(c => c.Item1 == targetMatch.Item1) > 1)
                {
                    TraceWriteLine($"Rest Server {m_ServerName} macthed multiple endpoints for request to uri {requestUri.AbsoluteUri}. Macthed endpoints :({macthedEndpoints.Select(c => c.Address).Aggregate((c1, c2) => c1 + ", " + c2)})\nArbitrarily picking {restEndpoint.Address} as target endpoint, couldn't resolve with fewest macthed variables.");
                }
                else
                {
                    TraceWriteLine($"Rest Server {m_ServerName} macthed multiple endpoints for request to uri {requestUri.AbsoluteUri}. Macthed endpoints :({macthedEndpoints.Select(c => c.Address).Aggregate((c1, c2) => c1 + ", " + c2)})\nPicking {restEndpoint.Address} as target endpoint with fewest macthed variables.");
                }
            }

            if (restEndpoint != null)
            {
                object returnObject = null;
                object payload = null;
                if (context.Request.HasEntityBody)
                {
                    string contentBody = string.Empty;
                    StreamReader reader = null;
                    try
                    {
                        Stream body = context.Request.InputStream;
                        Encoding encoding = context.Request.ContentEncoding;
                        reader = new StreamReader(body, encoding);
                        contentBody = reader.ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 500;
                        TraceWriteLine($"Failed to read from the input stream for request to uri {requestUri.AbsoluteUri}\n{ex.ToString()}");
                        context.Response.OutputStream.Close();
                        return;
                    }

                    TraceWriteLine($"Rest Server {m_ServerName} received request to uri {requestUri.AbsoluteUri} with payload: {contentBody}");

                    try
                    {
                        string contentType = context.Request.ContentType;
                        if (string.IsNullOrEmpty(contentType))
                        {
                            contentType = restEndpoint.ContentType.ToLower();
                        }

                        //parse the imput stream and serealize
                        if (contentType == MIME_TYPE_JSON || contentType.Contains(MIME_TYPE_JSON))
                        {
                            payload = JObject.Parse(contentBody);
                            //payload = JsonConvert.DeserializeObject(contentBody, PayloadType);
                        }
                        else if (contentType == MIME_TYPE_XML || contentType.Contains(MIME_TYPE_XML))
                        {
                            payload = XDocument.Parse(contentType);
                            //XmlSerializer xmlSerializer = new XmlSerializer(PayloadType);
                            //payload = xmlSerializer.Deserialize(reader);
                        }
                        else if (contentType == MIME_TYPE_PLAIN || contentType.Contains(MIME_TYPE_PLAIN))
                        {
                            if (restEndpoint.ContentType.ToLower() == MIME_TYPE_JSON || restEndpoint.ContentType.ToLower().Contains(MIME_TYPE_JSON))
                            {
                                payload = JObject.Parse(contentBody);
                                //payload = JsonConvert.DeserializeObject(contentBody, PayloadType);
                            }
                            else if (restEndpoint.ContentType.ToLower() == MIME_TYPE_XML || restEndpoint.ContentType.ToLower().Contains(MIME_TYPE_XML))
                            {
                                payload = XDocument.Parse(contentType);
                                //XmlSerializer xmlSerializer = new XmlSerializer(PayloadType);
                                //payload = xmlSerializer.Deserialize(reader);
                            }
                            else
                            {
                                context.Response.StatusCode = 415;
                                TraceWriteLine(string.Format("Failed to deserialize payload for request to uri {0}, request content was type {1} and the endpoint didn't have a defined content type", requestUri.AbsoluteUri, context.Request.ContentType));
                                context.Response.OutputStream.Close();
                                return;
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = 415;
                            TraceWriteLine(string.Format("Failed to deserialize payload for request to uri {0}, unknown content type. request content was type {1}, endpoint content type was {2}", requestUri.AbsoluteUri, context.Request.ContentType, restEndpoint.ContentType));
                            context.Response.OutputStream.Close();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 500;
                        TraceWriteLine(string.Format("Failed to deserialize payload for request to uri {0}, request content was type {1}, endpoint content type was {2}\n{3}", requestUri.AbsoluteUri, context.Request.ContentType, restEndpoint.ContentType, ex.ToString()));
                        context.Response.OutputStream.Close();
                        return;
                    }
                }

                int customResponseStatusCode = -1;
                try
                {
                    lock (restEndpoint.PatternData)
                    {
                        //do not use timeout
                        if (payload is JObject)
                            returnObject = restEndpoint.PatternData.ResponseData.GetResponse(payload as JObject);
                        else if (payload is XDocument)
                            returnObject = restEndpoint.PatternData.ResponseData.GetResponse(payload as XDocument);
                        else
                            returnObject = restEndpoint.PatternData.ResponseData.GetResponse();
                    }
                }
                catch (Exception ex)
                {
                    TraceWriteLine($"Rest Endpoint {restEndpoint.EndpointData.Name} target method threw an exception during execution.\n{ex.ToString()}");
                }

                //use custom response code if one was set
                context.Response.StatusCode = customResponseStatusCode != -1 ? customResponseStatusCode : 200;

                if (returnObject != null)
                {
                    try
                    {
                        string contentType = restEndpoint.ContentType.ToLower();
                        if (context.Request.AcceptTypes != null &&
                            (context.Request.AcceptTypes.Contains(MIME_TYPE_JSON) || context.Request.AcceptTypes.Contains(MIME_TYPE_XML)))
                        {
                            contentType = context.Request.AcceptTypes.Contains(MIME_TYPE_JSON) ? MIME_TYPE_JSON : MIME_TYPE_XML;
                        }

                        if (returnObject is string)
                        {
                            string returnString = returnObject as string;
                            if (returnString != string.Empty)
                            {
                                byte[] contentBuffer = Encoding.UTF8.GetBytes(returnString);
                                context.Response.ContentLength64 = contentBuffer.Length;
                                context.Response.OutputStream.Write(contentBuffer, 0, contentBuffer.Length);
                            }
                            else
                            {
                                //fall out of try and close response stream, nothing to write
                            }
                        }
                        //else
                        //{
                        //    //serialize returnObject and add it the response   
                        //    if (contentType == MIME_TYPE_XML)
                        //    {
                        //        context.Response.ContentType = MIME_TYPE_XML;

                        //        XmlSerializer xmlSerializer = new XmlSerializer(ResponseType);
                        //        xmlSerializer.Serialize(context.Response.OutputStream, returnObject);
                        //    }
                        //    else //(contentType == MIME_TYPE_JSON)
                        //    {
                        //        context.Response.ContentType = MIME_TYPE_JSON;

                        //        string responseContent = JsonConvert.SerializeObject(returnObject);
                        //        byte[] contentBuffer = Encoding.UTF8.GetBytes(responseContent);
                        //        context.Response.ContentLength64 = contentBuffer.Length;
                        //        context.Response.OutputStream.Write(contentBuffer, 0, contentBuffer.Length);
                        //        Trace.WriteLine($"Responding to request to uri {requestUri.AbsoluteUri} with response payload: {responseContent}");
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 500;
                        TraceWriteLine(string.Format("Failed to serialize response for request to uri {0}.\n{1}", requestUri.AbsoluteUri, ex.ToString()));
                    }
                    finally
                    {
                        context.Response.OutputStream.Close();
                    }
                }
                //else if (ResponseType == typeof(void))
                //{
                //    context.Response.OutputStream.Close();
                //}
                else
                {
                    context.Response.StatusCode = 500;
                    TraceWriteLine($"Rest Endpoint {restEndpoint.EndpointData.Name} target method returned null.");
                    context.Response.OutputStream.Close();
                }
            }
            else
            {
                //return 404
                context.Response.StatusCode = 404;
                TraceWriteLine(string.Format("Rest Server {0} received request that did not match a configured endpoint for uri {1} with http method {2}", m_ServerName, requestUri.AbsoluteUri, context.Request.HttpMethod));
                context.Response.OutputStream.Close();
                return;
            }

            return;
        }

        private void TraceWriteLine(string message)
        {
            OnRestServerTrace?.Invoke(this, new OnRestServerTraceEventArgs(m_RestServerData, message));
        }
    }

    /// <summary>
    /// A rest endpoint for parsing configurations
    /// </summary>
    public class RestEndpoint// : IRestJobOwner
    {
        private string m_BaseAddress = string.Empty;
        private string m_Address = string.Empty;
        private string m_ContentType = string.Empty;
        private string m_HttpMethod = string.Empty;
        private int m_Timeout = -1;
        private EndpointData m_RestJobConfig = null;
        private PatternData m_Pattern = null;

        /// <summary>
        /// The type of the UriArgs parameter
        /// </summary>
        public static readonly Type PARAMETER_ARGS_TYPE = typeof(NameValueCollection);
        /// <summary>
        /// The name of the payload parameter
        /// </summary>
        public static readonly string PAYLOAD_PARAM_NAME = "payload";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlConfig"></param>
        /// <param name="baseAddress"></param>
        public RestEndpoint(EndpointData data, PatternData pattern, string baseAddress)
        {
            m_RestJobConfig = data;
            m_Pattern = pattern;

            m_BaseAddress = baseAddress;

            m_Address = data.HttpEndpointAddress;//xmlConfig["endpoint_address", string.Empty].Trim();
            if (m_Address == string.Empty)
            {
                throw new EndpointDataException(data,
                        "HttpEndpointAddress",
                        "endpoint address is required");
            }

            m_ContentType = DataEnums.GetDescriptorText(pattern.ResponseData.ContentType);//xmlConfig["content_type", string.Empty].Trim();
            if (m_ContentType == string.Empty)
            {
                throw new EndpointDataException(data,
                        "ContentType",
                        "content type is required");
            }
            else
            {
                if (m_ContentType.ToLower() == "json")
                    m_ContentType = RestServer.MIME_TYPE_JSON;
                if (m_ContentType.ToLower() == "xml")
                    m_ContentType = RestServer.MIME_TYPE_XML;
            }

            m_HttpMethod = DataEnums.GetDescriptorText(pattern.HttpMethod);//xmlConfig["http_method", string.Empty].Trim();

            m_Timeout = -1;//xmlConfig["timeout", -1];

            //XmlHelper restJob = xmlConfig.FindNodeHelper("rest_job");
            //m_RestJobConfig = restJob;

            //m_Assembly =
            //restJob["job_assembly", String.Empty].Trim();
            //Assembly endpointAssembly;   // If 'session_assembly' attribute
            //                             // found,
            //if (m_Assembly.Length > 0)
            //{
            //    // If assembly file does not contain a
            //    // path,
            //    if (m_Assembly.IndexOf(
            //        Path.DirectorySeparatorChar) < 0)
            //    {
            //        // Prepend path for current application
            //        m_Assembly = PlatformCore.AppBaseDirectory +
            //            m_Assembly;
            //    }
            //    try                     // Load assembly from specified  file
            //    {
            //        endpointAssembly = Assembly.LoadFrom(
            //            m_Assembly);
            //    }
            //    catch (Exception innerException)
            //    {
            //        // Throw exception without logging it
            //        throw new XmlHelperException(innerException,
            //            restJob.AttributeSource("job_assembly"),
            //            "Exception loading assembly from file '" +
            //            m_Assembly + "':  " +
            //            innerException.Message);
            //    }
            //}
            //else                        // Else 'session_assembly' attribute not
            //{                           // found
            //                            // Get currently executing assembly
            //    endpointAssembly = Assembly.GetExecutingAssembly();
            //}
            //// Get session class
            //m_Class =
            //    restJob["job_class", String.Empty].Trim();
            //// If missing 'session_class'
            //// attribute,
            //if (m_Class.Length == 0)
            //{
            //    // Throw exception without logging it
            //    throw new XmlHelperException(null,
            //        restJob.AttributeSource("job_class"), "Element <" +
            //        xmlConfig.Name + "> missing 'session_class' attribute");
            //}
            //// If session class full name does not
            //// contain '.',
            //if (m_Class.IndexOf('.') < 0)
            //{
            //    // Prepend default path to class name
            //    m_Class =
            //        "Intelligrated.Common.CommLibrary." +
            //        m_Class;
            //}
            //// Get type for session class
            //try
            //{
            //    m_ClassType = endpointAssembly.GetType(
            //        m_Class);
            //}
            //catch (Exception innerException)
            //{
            //    // Throw exception without logging it
            //    throw new XmlHelperException(innerException,
            //        restJob.AttributeSource("job_class"),
            //        "Exception getting class type for " + m_Class +
            //        ":  " + innerException.Message);
            //}
            //// If class not found,
            //if (m_ClassType == null)
            //{
            //    // Throw exception without logging it
            //    throw new XmlHelperException(null,
            //        restJob.AttributeSource("job_class"),
            //        "Could not find class '" + m_Class +
            //        "' in assembly '" + endpointAssembly.FullName + "'");
            //}

            //m_Method = restJob["job_method", string.Empty].Trim();
            //if (m_Method == string.Empty)
            //{
            //    throw new XmlHelperException(null,
            //            restJob.AttributeSource("job_method"),
            //            "endpoint method is a required attribute");
            //}

            //if (m_ClassType.IsSubclassOf(typeof(RestJob)))
            //{
            //    m_RestJobInstance = (RestJob)Activator.CreateInstance(m_ClassType);
            //}
            //else
            //{
            //    throw new ArgumentException(string.Format("Class {0} doesn't inherit from RestJob", m_ClassType.FullName));
            //}

            ////Check that method has no additional parameters
            //if(ValidateMethod() == false)
            //{
            //    throw new ArgumentException(string.Format("Method {0} has invalid parameters. UriArgs parameter must be of type NameValueCollection and payload parameter must be named payload, there can not be more than two parameters.", m_Method));
            //}
        }

        //Public Properties

        /// <summary>
        /// The address of the endpoint
        /// </summary>
        public string Address
        {
            get
            {
                return m_Address;
            }
        }

        /// <summary>
        /// Template address
        /// </summary>
        public UriTemplate TemplateAddress
        {
            get
            {
                return new UriTemplate(m_Address);
            }
        }

        /// <summary>
        /// Is the address a template address
        /// </summary>
        public bool IsTemplateAddress
        {
            get
            {
                if (m_Address.Contains("{") && m_Address.Contains("}"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Is the address a query address
        /// </summary>
        public bool IsQueryAddress
        {
            get
            {
                return m_Address.Contains("?");
            }
        }

        /// <summary>
        /// A substring of the address before any template variables
        /// </summary>
        public string PreTemplateAddress
        {
            get
            {
                return m_Address.Substring(0, m_Address.IndexOf('{'));
            }
        }

        /// <summary>
        /// A substring of the address before the query string
        /// </summary>
        public string PreQueryAddress
        {
            get
            {
                return m_Address.Substring(0, m_Address.IndexOf("?"));
            }
        }

        /// <summary>
        /// The content type of the endpoint
        /// </summary>
        public string ContentType
        {
            get
            {
                return m_ContentType;
            }
        }

        /// <summary>
        /// The timeout in ms, returns a -1 if there is no configured timeout
        /// </summary>
        public int EndpointTimeout
        {
            get
            {
                return m_Timeout;
            }
        }

        /// <summary>
        /// The Timeout of the endpoint
        /// </summary>
        public int Timeout
        {
            get
            {
                return m_Timeout;
            }
        }

        /// <summary>
        /// The HttpMethod for the endpoint
        /// </summary>
        public string HttpMethod
        {
            get
            {
                return m_HttpMethod;
            }
        }

        /// <summary>
        /// Always false, the auth is supported by the server not the endpoint.
        /// </summary>
        public bool UseBasicAuthentication
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Always empty, the auth is supported by the server not the endpoint.
        /// </summary>
        public string AuthenticationToken
        {
            get
            {
                return string.Empty;
            }
        }

        public EndpointData EndpointData
        {
            get
            {
                return m_RestJobConfig;
            }
        }

        public PatternData PatternData
        {
            get
            {
                return m_Pattern;
            }
        }

        //Private Methods

        private UriTemplateMatch GetUriTemplateMatch(HttpListenerRequest request)
        {
            if (m_HttpMethod != string.Empty && 
                request.HttpMethod.ToLower() != m_HttpMethod.ToLower())
            {
                //Wrong http method, return null
                return null;
            }

            Uri fullUri = new Uri(request.Url.AbsoluteUri);
            try
            {
                if (fullUri.Query == string.Empty)
                {
                    if ((fullUri.AbsolutePath.Last() == '/' || fullUri.AbsolutePath.Last() == '\\'))
                    {
                        UriTemplate localTemplate = TemplateAddress;
                        if (m_Address.Last() != '/' && m_Address.Last() != '\\')
                        {
                            localTemplate = new UriTemplate(m_Address + '/');
                        }

                        Uri prefixUri = new Uri(m_BaseAddress);
                        UriTemplateMatch uriMatch = localTemplate.Match(prefixUri, fullUri);
                        return uriMatch;
                    }
                    else
                    {
                        UriTemplate localTemplate = TemplateAddress;
                        if (m_Address.Last() == '/' || m_Address.Last() == '\\')
                        {
                            localTemplate = new UriTemplate(new string(m_Address.Take(m_Address.Length - 1).ToArray()));
                        }

                        Uri prefixUri = new Uri(m_BaseAddress);
                        UriTemplateMatch uriMatch = localTemplate.Match(prefixUri, fullUri);
                        return uriMatch;
                    }
                }
                else
                {
                    Uri prefixUri = new Uri(m_BaseAddress);
                    UriTemplateMatch uriMatch = TemplateAddress.Match(prefixUri, fullUri);
                    return uriMatch;
                }
            }
            catch
            {
                return null;
            }
        }

        //Public Methods

        /// <summary>
        /// Returns true if the endpoint is a match for the request 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool RequestMatch(HttpListenerRequest request)
        {
            return GetUriTemplateMatch(request) != null;
        }

        /// <summary>
        /// Gets the number of variables matched in the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int TemplateMatchCount(HttpListenerRequest request)
        {
            UriTemplateMatch match = GetUriTemplateMatch(request);
            if (match != null)
                return match.BoundVariables.Count;
            else
                return -1;
        }

        /// <summary>
        /// Gets the parse template or query string varaibles
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public NameValueCollection GetParameterArgs(HttpListenerRequest request)
        {
            return GetUriTemplateMatch(request).BoundVariables;
        }
    }
}
