using Cellsynt.Sms.Exceptions;
using System.Net;

namespace Cellsynt.Sms
{
    internal static class ApiResponseParser
    {
        public static SendResult Parse(string response, HttpStatusCode statusCode)
        {
            if (string.IsNullOrEmpty(response))
            {
                throw new SmsSendException("Empty response", statusCode);
            }

            const string okPrefix = "OK: ";
            if (response.StartsWith(okPrefix))
            {
                string[] trackingIds = response.Substring(okPrefix.Length).Split(',');
                return new SendResult(trackingIds);
            }

            const string errorPrefix = "Error: ";
            if (response.StartsWith(errorPrefix))
            {
                string message = response.Substring(errorPrefix.Length);

                if (message == "Out of credits")
                {
                    throw new OutOfCreditsException();
                }

                throw new SmsSendException(message, statusCode);
            }

            throw new SmsSendException($"Unknown response: \"{response}\"", statusCode);
        }
    }
}
