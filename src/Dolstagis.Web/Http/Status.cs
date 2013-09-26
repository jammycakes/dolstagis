using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Http
{
    public sealed class Status
    {
        public int Code { get; private set; }

        public string Description { get; private set; }

        private Status(int code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        // The values are based on the list found at http://en.wikipedia.org/wiki/List_of_HTTP_status_codes

        public static Status Continue = new Status(100, "Continue");
        public static Status SwitchingProtocols = new Status(101, "Switching Protocols");
        public static Status Processing = new Status(102, "Processing");
        public static Status Checkpoint = new Status(103, "Checkpoint");
        public static Status OK = new Status(200, "OK");
        public static Status Created = new Status(201, "Created");
        public static Status Accepted = new Status(202, "Accepted");
        public static Status NonAuthoritativeInformation = new Status(203, "Non Authoritative Information");
        public static Status NoContent = new Status(204, "No Content");
        public static Status ResetContent = new Status(205, "Reset Content");
        public static Status PartialContent = new Status(206, "Partial Content");
        public static Status MultipleStatus = new Status(207, "Multiple Status");
        public static Status IMUsed = new Status(226, "IM Used");
        public static Status MultipleChoices = new Status(300, "Multiple Choices");
        public static Status MovedPermanently = new Status(301, "Moved Permanently");
        public static Status Found = new Status(302, "Found");
        public static Status SeeOther = new Status(303, "See Other");
        public static Status NotModified = new Status(304, "Not Modified");
        public static Status UseProxy = new Status(305, "Use Proxy");
        public static Status SwitchProxy = new Status(306, "Switch Proxy");
        public static Status TemporaryRedirect = new Status(307, "Temporary Redirect");
        public static Status ResumeIncomplete = new Status(308, "Resume Incomplete");
        public static Status BadRequest = new Status(400, "Bad Request");
        public static Status Unauthorized = new Status(401, "Unauthorized");
        public static Status PaymentRequired = new Status(402, "Payment Required");
        public static Status Forbidden = new Status(403, "Forbidden");
        public static Status NotFound = new Status(404, "Not Found");
        public static Status MethodNotAllowed = new Status(405, "Method Not Allowed");
        public static Status NotAcceptable = new Status(406, "Not Acceptable");
        public static Status ProxyAuthenticationRequired = new Status(407, "Proxy Authentication Required");
        public static Status RequestTimeout = new Status(408, "Request Timeout");
        public static Status Conflict = new Status(409, "Conflict");
        public static Status Gone = new Status(410, "Gone");
        public static Status LengthRequired = new Status(411, "Length Required");
        public static Status PreconditionFailed = new Status(412, "Precondition Failed");
        public static Status RequestEntityTooLarge = new Status(413, "Request Entity Too Large");
        public static Status RequestUriTooLong = new Status(414, "Request Uri Too Long");
        public static Status UnsupportedMediaType = new Status(415, "Unsupported Media Type");
        public static Status RequestedRangeNotSatisfiable = new Status(416, "Requested Range Not Satisfiable");
        public static Status ExpectationFailed = new Status(417, "Expectation Failed");
        public static Status ImATeapot = new Status(418, "Im ATeapot");
        public static Status EnhanceYourCalm = new Status(420, "Enhance Your Calm");
        public static Status UnprocessableEntity = new Status(422, "Unprocessable Entity");
        public static Status Locked = new Status(423, "Locked");
        public static Status FailedDependency = new Status(424, "Failed Dependency");
        public static Status UnorderedCollection = new Status(425, "Unordered Collection");
        public static Status UpgradeRequired = new Status(426, "Upgrade Required");
        public static Status TooManyRequests = new Status(429, "Too Many Requests");
        public static Status NoResponse = new Status(444, "No Response");
        public static Status RetryWith = new Status(449, "Retry With");
        public static Status BlockedByWindowsParentalControls = new Status(450, "Blocked By Windows Parental Controls");
        public static Status ClientClosedRequest = new Status(499, "Client Closed Request");
        public static Status InternalServerError = new Status(500, "Internal Server Error");
        public static Status NotImplemented = new Status(501, "Not Implemented");
        public static Status BadGateway = new Status(502, "Bad Gateway");
        public static Status ServiceUnavailable = new Status(503, "Service Unavailable");
        public static Status GatewayTimeout = new Status(504, "Gateway Timeout");
        public static Status HttpVersionNotSupported = new Status(505, "Http Version Not Supported");
        public static Status VariantAlsoNegotiates = new Status(506, "Variant Also Negotiates");
        public static Status InsufficientStorage = new Status(507, "Insufficient Storage");
        public static Status BandwidthLimitExceeded = new Status(509, "Bandwidth Limit Exceeded");
        public static Status NotExtended = new Status(510, "Not Extended");
    }
}
