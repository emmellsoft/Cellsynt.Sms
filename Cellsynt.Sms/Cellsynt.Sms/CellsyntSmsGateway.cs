using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cellsynt.Sms
{
    internal class CellsyntSmsGateway : ICellsyntSmsGateway
    {
        private readonly ApiParamsBuilder _paramsBuilder;
        private static readonly Uri CellsyntSmsApiUri = new Uri("https://se-1.cellsynt.net/sms.php");

        public CellsyntSmsGateway(CellsyntCredentials credentials)
        {
            _paramsBuilder = new ApiParamsBuilder(credentials);
        }

        public async Task<SendResult> Send(SmsOriginator originator, SmsMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Validate();

            IDictionary<string, string> parameters = _paramsBuilder.GetParameters(originator, message);

            (string response, HttpStatusCode statusCode) = await Send(parameters);

            return ApiResponseParser.Parse(response, statusCode);
        }

        protected virtual async Task<(string, HttpStatusCode)> Send(IDictionary<string, string> parameters)
        {
            using (var httpClient = new HttpClient())
            {
                var uriBuilder = new UriBuilder(CellsyntSmsApiUri)
                {
                    Query = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}"))
                };

                using (var response = await httpClient.PostAsync(uriBuilder.Uri, new StringContent(string.Empty)))
                {
                    return (await response.Content.ReadAsStringAsync(), response.StatusCode);
                }
            }
        }
    }
}
