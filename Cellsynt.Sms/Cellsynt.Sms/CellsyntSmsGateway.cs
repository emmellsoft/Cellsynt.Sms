using System.Threading.Tasks;

namespace Cellsynt.Sms
{
    internal class CellsyntSmsGateway : ICellsyntSmsGateway
    {
        public CellsyntSmsGateway(OriginatorKind originatorKind, string originator)
        {
        }

        public Task<SendResult> Send(SmsMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}
