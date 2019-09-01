using Cellsynt.Sms.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Cellsynt.Sms
{
    public abstract class SmsMessage
    {
        protected SmsMessage()
        {
        }

        protected SmsMessage(string destination)
        {
            Destinations.Add(destination);
        }

        public List<string> Destinations { get; } = new List<string>();

        public virtual void Validate()
        {
            if (Destinations.Count == 0)
            {
                throw new SmsValidationException(SmsValidationErrorCode.NoDestinations);
            }

            if (Destinations.Count > 25000)
            {
                throw new SmsValidationException(SmsValidationErrorCode.TooManyDestinations);
            }

            foreach (string destination in Destinations)
            {
                if (string.IsNullOrEmpty(destination))
                {
                    throw new SmsValidationException(SmsValidationErrorCode.EmptyDestination);
                }

                if (destination.Length > 17)
                {
                    throw new SmsValidationException(SmsValidationErrorCode.TooLongDestination);
                }

                if (!destination.StartsWith("00"))
                {
                    throw new SmsValidationException(SmsValidationErrorCode.IllegalDestinationPrefix);
                }

                if (destination.Any(c => (c < '0') || (c > '9')))
                {
                    throw new SmsValidationException(SmsValidationErrorCode.NonNumericalDestination);
                }
            }
        }
    }
}
