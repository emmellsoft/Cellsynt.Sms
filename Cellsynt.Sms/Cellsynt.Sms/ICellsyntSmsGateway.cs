using System.Threading.Tasks;

namespace Cellsynt.Sms
{
    public interface ICellsyntSmsGateway
    {
        Task<SendResult> Send(SmsMessage message);
    }
}
