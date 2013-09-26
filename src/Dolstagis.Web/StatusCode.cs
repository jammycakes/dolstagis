using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web
{
    public sealed class StatusCode
    {
        public int Code { get; private set; }

        public string Description { get; private set; }

        private StatusCode(int code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        // The values are based on the list found at http://en.wikipedia.org/wiki/List_of_HTTP_status_codes

        public static StatusCode Continue = new StatusCode(100, "Continue");
        public static StatusCode SwitchingProtocols = new StatusCode(101, "Switching Protocols");
        public static StatusCode Processing = new StatusCode(102, "Processing");
        public static StatusCode Checkpoint = new StatusCode(103, "Checkpoint");
        public static StatusCode OK = new StatusCode(200, "OK");
        public static StatusCode Created = new StatusCode(201, "Created");
        public static StatusCode Accepted = new StatusCode(202, "Accepted");
        public static StatusCode NonAuthoritativeInformation = new StatusCode(203, "Non Authoritative Information");
        public static StatusCode NoContent = new StatusCode(204, "No Content");
        public static StatusCode ResetContent = new StatusCode(205, "Reset Content");
        public static StatusCode PartialContent = new StatusCode(206, "Partial Content");
        public static StatusCode MultipleStatus = new StatusCode(207, "Multiple Status");
        public static StatusCode IMUsed = new StatusCode(226, "IM Used");
        public static StatusCode MultipleChoices = new StatusCode(300, "Multiple Choices");
        public static StatusCode MovedPermanently = new StatusCode(301, "Moved Permanently");
        public static StatusCode Found = new StatusCode(302, "Found");
        public static StatusCode SeeOther = new StatusCode(303, "See Other");
        public static StatusCode NotModified = new StatusCode(304, "Not Modified");
        public static StatusCode UseProxy = new StatusCode(305, "Use Proxy");
        public static StatusCode SwitchProxy = new StatusCode(306, "Switch Proxy");
        public static StatusCode TemporaryRedirect = new StatusCode(307, "Temporary Redirect");
        public static StatusCode ResumeIncomplete = new StatusCode(308, "Resume Incomplete");
        public static StatusCode BadRequest = new StatusCode(400, "Bad Request");
        public static StatusCode Unauthorized = new StatusCode(401, "Unauthorized");
        public static StatusCode PaymentRequired = new StatusCode(402, "Payment Required");
        public static StatusCode Forbidden = new StatusCode(403, "Forbidden");
        public static StatusCode NotFound = new StatusCode(404, "Not Found");
        public static StatusCode MethodNotAllowed = new StatusCode(405, "Method Not Allowed");
        public static StatusCode NotAcceptable = new StatusCode(406, "Not Acceptable");
        public static StatusCode ProxyAuthenticationRequired = new StatusCode(407, "Proxy Authentication Required");
        public static StatusCode RequestTimeout = new StatusCode(408, "Request Timeout");
        public static StatusCode Conflict = new StatusCode(409, "Conflict");
        public static StatusCode Gone = new StatusCode(410, "Gone");
        public static StatusCode LengthRequired = new StatusCode(411, "Length Required");
        public static StatusCode PreconditionFailed = new StatusCode(412, "Precondition Failed");
        public static StatusCode RequestEntityTooLarge = new StatusCode(413, "Request Entity Too Large");
        public static StatusCode RequestUriTooLong = new StatusCode(414, "Request Uri Too Long");
        public static StatusCode UnsupportedMediaType = new StatusCode(415, "Unsupported Media Type");
        public static StatusCode RequestedRangeNotSatisfiable = new StatusCode(416, "Requested Range Not Satisfiable");
        public static StatusCode ExpectationFailed = new StatusCode(417, "Expectation Failed");
        public static StatusCode ImATeapot = new StatusCode(418, "Im ATeapot");
        public static StatusCode EnhanceYourCalm = new StatusCode(420, "Enhance Your Calm");
        public static StatusCode UnprocessableEntity = new StatusCode(422, "Unprocessable Entity");
        public static StatusCode Locked = new StatusCode(423, "Locked");
        public static StatusCode FailedDependency = new StatusCode(424, "Failed Dependency");
        public static StatusCode UnorderedCollection = new StatusCode(425, "Unordered Collection");
        public static StatusCode UpgradeRequired = new StatusCode(426, "Upgrade Required");
        public static StatusCode TooManyRequests = new StatusCode(429, "Too Many Requests");
        public static StatusCode NoResponse = new StatusCode(444, "No Response");
        public static StatusCode RetryWith = new StatusCode(449, "Retry With");
        public static StatusCode BlockedByWindowsParentalControls = new StatusCode(450, "Blocked By Windows Parental Controls");
        public static StatusCode ClientClosedRequest = new StatusCode(499, "Client Closed Request");
        public static StatusCode InternalServerError = new StatusCode(500, "Internal Server Error");
        public static StatusCode NotImplemented = new StatusCode(501, "Not Implemented");
        public static StatusCode BadGateway = new StatusCode(502, "Bad Gateway");
        public static StatusCode ServiceUnavailable = new StatusCode(503, "Service Unavailable");
        public static StatusCode GatewayTimeout = new StatusCode(504, "Gateway Timeout");
        public static StatusCode HttpVersionNotSupported = new StatusCode(505, "Http Version Not Supported");
        public static StatusCode VariantAlsoNegotiates = new StatusCode(506, "Variant Also Negotiates");
        public static StatusCode InsufficientStorage = new StatusCode(507, "Insufficient Storage");
        public static StatusCode BandwidthLimitExceeded = new StatusCode(509, "Bandwidth Limit Exceeded");
        public static StatusCode NotExtended = new StatusCode(510, "Not Extended");
    }
}
