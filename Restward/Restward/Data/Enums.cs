using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restward.Data
{
    public enum ContentType
    {
        [Description("application/json")]
        json = 0,
        [Description("application/xml")]
        xml
    }

    public enum HttpMethod
    {
        [Description("GET")]
        Get = 0,
        [Description("POST")]
        Post,
        [Description("PUT")]
        Put,
        [Description("PATCH")]
        Patch,
        [Description("DELETE")]
        Delete,
        [Description("COPY")]
        Copy,
        [Description("HEAD")]
        Head,
        [Description("OPTIONS")]
        Options,
        [Description("LINK")]
        Link,
        [Description("UNLINK")]
        Unlink,
        [Description("PURGE")]
        Purge,
        [Description("LOCK")]
        Lock,
        [Description("UNLOCK")]
        Unlock,
        [Description("PROPFIND")]
        Propfind,
        [Description("VIEW")]
        View
    }

    //
    // Summary:
    //     Contains the values of status codes defined for HTTP.
    public enum HttpStatusCode
    {
        //
        // Summary:
        //     Equivalent to HTTP status 100. System.Net.HttpStatusCode.Continue indicates that
        //     the client can continue with its request.
        [Description("Continue(100)")]
        Continue = 100,
        //
        // Summary:
        //     Equivalent to HTTP status 101. System.Net.HttpStatusCode.SwitchingProtocols indicates
        //     that the protocol version or protocol is being changed.
        [Description("SwitchingProtocols(101)")]
        SwitchingProtocols = 101,
        //
        // Summary:
        //     Equivalent to HTTP status 200. System.Net.HttpStatusCode.OK indicates that the
        //     request succeeded and that the requested information is in the response. This
        //     is the most common status code to receive.
        [Description("OK(200)")]
        OK = 200,
        //
        // Summary:
        //     Equivalent to HTTP status 201. System.Net.HttpStatusCode.Created indicates that
        //     the request resulted in a new resource created before the response was sent.
        [Description("Created(201)")]
        Created = 201,
        //
        // Summary:
        //     Equivalent to HTTP status 202. System.Net.HttpStatusCode.Accepted indicates that
        //     the request has been accepted for further processing.
        [Description("Accepted(202)")]
        Accepted = 202,
        //
        // Summary:
        //     Equivalent to HTTP status 203. System.Net.HttpStatusCode.NonAuthoritativeInformation
        //     indicates that the returned metainformation is from a cached copy instead of
        //     the origin server and therefore may be incorrect.
        [Description("NonAuthoritativeInformation(203)")]
        NonAuthoritativeInformation = 203,
        //
        // Summary:
        //     Equivalent to HTTP status 204. System.Net.HttpStatusCode.NoContent indicates
        //     that the request has been successfully processed and that the response is intentionally
        //     blank.
        [Description("NoContent(204)")]
        NoContent = 204,
        //
        // Summary:
        //     Equivalent to HTTP status 205. System.Net.HttpStatusCode.ResetContent indicates
        //     that the client should reset (not reload) the current resource.
        [Description("ResetContent(205)")]
        ResetContent = 205,
        //
        // Summary:
        //     Equivalent to HTTP status 206. System.Net.HttpStatusCode.PartialContent indicates
        //     that the response is a partial response as requested by a GET request that includes
        //     a byte range.
        [Description("PartialContent(206)")]
        PartialContent = 206,
        //
        // Summary:
        //     Equivalent to HTTP status 300. System.Net.HttpStatusCode.Ambiguous indicates
        //     that the requested information has multiple representations. The default action
        //     is to treat this status as a redirect and follow the contents of the Location
        //     header associated with this response.
        [Description("Ambiguous(300)")]
        Ambiguous = 300,
        //
        // Summary:
        //     Equivalent to HTTP status 301. System.Net.HttpStatusCode.Moved indicates that
        //     the requested information has been moved to the URI specified in the Location
        //     header. The default action when this status is received is to follow the Location
        //     header associated with the response. When the original request method was POST,
        //     the redirected request will use the GET method.
        [Description("Moved(301)")]
        Moved = 301,
        //
        // Summary:
        //     Equivalent to HTTP status 302. System.Net.HttpStatusCode.Redirect indicates that
        //     the requested information is located at the URI specified in the Location header.
        //     The default action when this status is received is to follow the Location header
        //     associated with the response. When the original request method was POST, the
        //     redirected request will use the GET method.
        [Description("Redirect(302)")]
        Redirect = 302,
        //
        // Summary:
        //     Equivalent to HTTP status 303. System.Net.HttpStatusCode.RedirectMethod automatically
        //     redirects the client to the URI specified in the Location header as the result
        //     of a POST. The request to the resource specified by the Location header will
        //     be made with a GET.
        [Description("RedirectMethod(303)")]
        RedirectMethod = 303,
        //
        // Summary:
        //     Equivalent to HTTP status 304. System.Net.HttpStatusCode.NotModified indicates
        //     that the client's cached copy is up to date. The contents of the resource are
        //     not transferred.
        [Description("NotModified(304)")]
        NotModified = 304,
        //
        // Summary:
        //     Equivalent to HTTP status 305. System.Net.HttpStatusCode.UseProxy indicates that
        //     the request should use the proxy server at the URI specified in the Location
        //     header.
        [Description("UseProxy(305)")]
        UseProxy = 305,
        //
        // Summary:
        //     Equivalent to HTTP status 306. System.Net.HttpStatusCode.Unused is a proposed
        //     extension to the HTTP/1.1 specification that is not fully specified.
        [Description("Unused(306)")]
        Unused = 306,
        //
        // Summary:
        //     Equivalent to HTTP status 307. System.Net.HttpStatusCode.TemporaryRedirect indicates
        //     that the request information is located at the URI specified in the Location
        //     header. The default action when this status is received is to follow the Location
        //     header associated with the response. When the original request method was POST,
        //     the redirected request will also use the POST method.
        [Description("TemporaryRedirect(307)")]
        TemporaryRedirect = 307,
        //
        // Summary:
        //     Equivalent to HTTP status 400. System.Net.HttpStatusCode.BadRequest indicates
        //     that the request could not be understood by the server. System.Net.HttpStatusCode.BadRequest
        //     is sent when no other error is applicable, or if the exact error is unknown or
        //     does not have its own error code.
        [Description("BadRequest(401)")]
        BadRequest = 400,
        //
        // Summary:
        //     Equivalent to HTTP status 401. System.Net.HttpStatusCode.Unauthorized indicates
        //     that the requested resource requires authentication. The WWW-Authenticate header
        //     contains the details of how to perform the authentication.
        [Description("Unauthorized(401)")]
        Unauthorized = 401,
        //
        // Summary:
        //     Equivalent to HTTP status 402. System.Net.HttpStatusCode.PaymentRequired is reserved
        //     for future use.
        [Description("PaymentRequired(402)")]
        PaymentRequired = 402,
        //
        // Summary:
        //     Equivalent to HTTP status 403. System.Net.HttpStatusCode.Forbidden indicates
        //     that the server refuses to fulfill the request.
        [Description("Forbidden(403)")]
        Forbidden = 403,
        //
        // Summary:
        //     Equivalent to HTTP status 404. System.Net.HttpStatusCode.NotFound indicates that
        //     the requested resource does not exist on the server.
        [Description("NotFound(404)")]
        NotFound = 404,
        //
        // Summary:
        //     Equivalent to HTTP status 405. System.Net.HttpStatusCode.MethodNotAllowed indicates
        //     that the request method (POST or GET) is not allowed on the requested resource.
        [Description("MethodNotAllowed(405)")]
        MethodNotAllowed = 405,
        //
        // Summary:
        //     Equivalent to HTTP status 406. System.Net.HttpStatusCode.NotAcceptable indicates
        //     that the client has indicated with Accept headers that it will not accept any
        //     of the available representations of the resource.
        [Description("NotAcceptable(406)")]
        NotAcceptable = 406,
        //
        // Summary:
        //     Equivalent to HTTP status 407. System.Net.HttpStatusCode.ProxyAuthenticationRequired
        //     indicates that the requested proxy requires authentication. The Proxy-authenticate
        //     header contains the details of how to perform the authentication.
        [Description("ProxyAuthenticationRequired(407)")]
        ProxyAuthenticationRequired = 407,
        //
        // Summary:
        //     Equivalent to HTTP status 408. System.Net.HttpStatusCode.RequestTimeout indicates
        //     that the client did not send a request within the time the server was expecting
        //     the request.
        [Description("RequestTimeout(408)")]
        RequestTimeout = 408,
        //
        // Summary:
        //     Equivalent to HTTP status 409. System.Net.HttpStatusCode.Conflict indicates that
        //     the request could not be carried out because of a conflict on the server.
        [Description("Conflict(409)")]
        Conflict = 409,
        //
        // Summary:
        //     Equivalent to HTTP status 410. System.Net.HttpStatusCode.Gone indicates that
        //     the requested resource is no longer available.
        [Description("Gone(410)")]
        Gone = 410,
        //
        // Summary:
        //     Equivalent to HTTP status 411. System.Net.HttpStatusCode.LengthRequired indicates
        //     that the required Content-length header is missing.
        [Description("LengthRequired(411)")]
        LengthRequired = 411,
        //
        // Summary:
        //     Equivalent to HTTP status 412. System.Net.HttpStatusCode.PreconditionFailed indicates
        //     that a condition set for this request failed, and the request cannot be carried
        //     out. Conditions are set with conditional request headers like If-Match, If-None-Match,
        //     or If-Unmodified-Since.
        [Description("PreconditionFailed(412)")]
        PreconditionFailed = 412,
        //
        // Summary:
        //     Equivalent to HTTP status 413. System.Net.HttpStatusCode.RequestEntityTooLarge
        //     indicates that the request is too large for the server to process.
        [Description("RequestEntityTooLarge(413)")]
        RequestEntityTooLarge = 413,
        //
        // Summary:
        //     Equivalent to HTTP status 414. System.Net.HttpStatusCode.RequestUriTooLong indicates
        //     that the URI is too long.
        [Description("RequestUriTooLong(414)")]
        RequestUriTooLong = 414,
        //
        // Summary:
        //     Equivalent to HTTP status 415. System.Net.HttpStatusCode.UnsupportedMediaType
        //     indicates that the request is an unsupported type.
        [Description("UnsupportedMediaType(415)")]
        UnsupportedMediaType = 415,
        //
        // Summary:
        //     Equivalent to HTTP status 416. System.Net.HttpStatusCode.RequestedRangeNotSatisfiable
        //     indicates that the range of data requested from the resource cannot be returned,
        //     either because the beginning of the range is before the beginning of the resource,
        //     or the end of the range is after the end of the resource.
        [Description("RequestedRangeNotSatisfiable(416)")]
        RequestedRangeNotSatisfiable = 416,
        //
        // Summary:
        //     Equivalent to HTTP status 417. System.Net.HttpStatusCode.ExpectationFailed indicates
        //     that an expectation given in an Expect header could not be met by the server.
        [Description("ExpectationFailed(417)")]
        ExpectationFailed = 417,
        //
        // Summary:
        //     Equivalent to HTTP status 426. System.Net.HttpStatusCode.UpgradeRequired indicates
        //     that the client should switch to a different protocol such as TLS/1.0.
        [Description("UpgradeRequired(426)")]
        UpgradeRequired = 426,
        //
        // Summary:
        //     Equivalent to HTTP status 500. System.Net.HttpStatusCode.InternalServerError
        //     indicates that a generic error has occurred on the server.
        [Description("InternalServerError(500)")]
        InternalServerError = 500,
        //
        // Summary:
        //     Equivalent to HTTP status 501. System.Net.HttpStatusCode.NotImplemented indicates
        //     that the server does not support the requested function.
        [Description("NotImplemented(501)")]
        NotImplemented = 501,
        //
        // Summary:
        //     Equivalent to HTTP status 502. System.Net.HttpStatusCode.BadGateway indicates
        //     that an intermediate proxy server received a bad response from another proxy
        //     or the origin server.
        [Description("BadGateway(502)")]
        BadGateway = 502,
        //
        // Summary:
        //     Equivalent to HTTP status 503. System.Net.HttpStatusCode.ServiceUnavailable indicates
        //     that the server is temporarily unavailable, usually due to high load or maintenance.
        [Description("ServiceUnavailable(503)")]
        ServiceUnavailable = 503,
        //
        // Summary:
        //     Equivalent to HTTP status 504. System.Net.HttpStatusCode.GatewayTimeout indicates
        //     that an intermediate proxy server timed out while waiting for a response from
        //     another proxy or the origin server.

        [Description("GatewayTimeout(504)")]
        GatewayTimeout = 504,
        //
        // Summary:
        //     Equivalent to HTTP status 505. System.Net.HttpStatusCode.HttpVersionNotSupported
        //     indicates that the requested HTTP version is not supported by the server.
        [Description("HttpVersionNotSupported(505)")]
        HttpVersionNotSupported = 505
    }

    public enum HttpProtocol
    {
        HTTP = 0,
        HTTPS
    }

    public static class DataEnums
    {
        public static EnumWrapper<T>[] GetEnumWrappers<T>()
        {
            List<EnumWrapper<T>> ret = new List<EnumWrapper<T>>();

            string[] names = Enum.GetNames(typeof(T));
            for (int i = 0; i < names.Length; i++)
            {
                EnumWrapper<T> newWrapper = new EnumWrapper<T>();
                newWrapper.Name = names[i];
                newWrapper.Value = (T)Enum.Parse(typeof(T), names[i]);

                DescriptionAttribute[] descAtts = typeof(T).GetField(names[i]).GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                if (descAtts != null && descAtts.Any())
                    newWrapper.Descriptor = descAtts.First().Description;
                else
                    newWrapper.Descriptor = names[i];

                ret.Add(newWrapper);
            }

            return ret.ToArray();
        }

        public static string GetDescriptorText<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            string enumName = Enum.GetName(typeof(T), value);

            DescriptionAttribute[] descAtts = typeof(T).GetField(enumName).GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (descAtts != null && descAtts.Any())
                return descAtts.First().Description;
            else
                return enumName;
        }

        public class EnumWrapper<T>
        {
            public string Name { get; set; }
            public string Descriptor { get; set; }
            public T Value { get; set; }
        }
    }
}
