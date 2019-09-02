using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cellsynt.Sms
{
    internal class CellsyntSmsGateway : ICellsyntSmsGateway
    {
        private readonly ApiBodyBuilder _bodyBuilder;
        private static readonly Uri CellsyntSmsApiUri = new Uri("https://se-1.cellsynt.net/sms.php");

        public CellsyntSmsGateway(CellsyntCredentials credentials)
        {
            _bodyBuilder = new ApiBodyBuilder(credentials);
        }

        public async Task<SendResult> Send(SmsOriginator originator, SmsMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Validate();

            string body = _bodyBuilder.GetBody(originator, message);

            (string response, HttpStatusCode statusCode) = await Send(body);

            return ApiResponseParser.Parse(response, statusCode);
        }

        protected virtual async Task<(string, HttpStatusCode)> Send(string body)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(CellsyntSmsApiUri, new StringContent(body)))
                {
                    return (await response.Content.ReadAsStringAsync(), response.StatusCode);
                }
            }
        }
    }
}
