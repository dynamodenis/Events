using System.Net;

namespace AptaEvents.Common.Helpers
{
    public class EventsPluginException : Exception
    {
        internal HttpStatusCode StatusCode { get; set; }

        internal EventsPluginException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
