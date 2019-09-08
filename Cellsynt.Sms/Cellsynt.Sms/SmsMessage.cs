using Cellsynt.Sms.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Cellsynt.Sms
{
    /// <summary>
    /// A generic SMS message
    /// </summary>
    public abstract class SmsMessage
    {
        /// <summary>
        /// The message destinations. Each destination should be a numerical string, including country prefix starting with 00, having a max length of 17 digits in total. The maximum number of destinations is 25000.
        /// </summary>
        public List<string> Destinations { get; } = new List<string>();

        /// <summary>
        /// Validate the message. Will automatically be performed when sent.
        /// <exception cref="SmsValidationException">Thrown when the message is not valid</exception>
        /// </summary>
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
