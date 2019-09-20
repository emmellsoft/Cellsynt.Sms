using System;
using System.Net;

namespace Cellsynt.Sms.Exceptions
{
    /// <summary>
    /// Failed SMS sending
    /// </summary>
    public class SmsSendException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message from the Cellsynt SMS host</param>
        /// <param name="statusCode">The HTTP status code returned by the Cellsynt SMS host</param>
        public SmsSendException(string message, HttpStatusCode statusCode)
            : base($"Code={statusCode}, Message={message}")
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// The HTTP status code returned by the Cellsynt SMS host
        /// </summary>
        public HttpStatusCode StatusCode { get; }
    }
}
