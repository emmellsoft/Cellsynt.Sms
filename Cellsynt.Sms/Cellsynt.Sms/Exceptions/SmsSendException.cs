using System;
using System.Net;

namespace Cellsynt.Sms.Exceptions
{
    public class SmsSendException : Exception
    {
        public SmsSendException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
