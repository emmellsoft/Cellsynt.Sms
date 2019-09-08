using System.Threading.Tasks;

namespace Cellsynt.Sms
{
    /// <summary>
    /// The Cellsynt SMS gateway
    /// </summary>
    public interface ICellsyntSmsGateway
    {
        /// <summary>
        /// Send an SMS
        /// </summary>
        /// <param name="originator">Optional originator</param>
        /// <param name="message">Message to send</param>
        /// <exception cref="Cellsynt.Sms.Exceptions.SmsSendException">Thrown when something failed during sending</exception>
        /// <exception cref="Cellsynt.Sms.Exceptions.OutOfCreditsException">Thrown when your Cellsynt account is out of credits</exception>
        /// <returns></returns>
        Task<SendResult> Send(Originator originator, SmsMessage message);
    }
}
