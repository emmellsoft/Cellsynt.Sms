using Cellsynt.Sms.Exceptions;

namespace Cellsynt.Sms
{
    /// <summary>
    /// A binary SMS message. Up to 140 bytes may be sent.
    /// </summary>
    public class BinarySmsMessage : SmsMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="destinations">The message destinations. Each destination should be a numerical string, including country prefix starting with 00, having a max length of 17 digits in total. The maximum number of destinations is 25000.</param>
        public BinarySmsMessage(params string[] destinations)
        {
            Destinations.AddRange(destinations);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">The binary data (optional). Up to 140 bytes in total (UserDataHeader + Data).</param>
        /// <param name="destinations">The message destinations. Each destination should be a numerical string, including country prefix starting with 00, having a max length of 17 digits in total. The maximum number of destinations is 25000.</param>
        public BinarySmsMessage(byte[] data, params string[] destinations)
        {
            Data = data;
            Destinations.AddRange(destinations);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userDataHeader">The binary user data header (optional). Up to 140 bytes in total (UserDataHeader + Data).</param>
        /// <param name="data">The binary data (optional). Up to 140 bytes in total (UserDataHeader + Data).</param>
        /// <param name="destinations">The message destinations. Each destination should be a numerical string, including country prefix starting with 00, having a max length of 17 digits in total. The maximum number of destinations is 25000.</param>
        public BinarySmsMessage(byte[] userDataHeader, byte[] data, params string[] destinations)
        {
            UserDataHeader = userDataHeader;
            Data = data;
            Destinations.AddRange(destinations);
        }

        /// <summary>
        /// The binary user data header (optional). Up to 140 bytes in total (UserDataHeader + Data).
        /// </summary>
        public byte[] UserDataHeader { get; set; }

        /// <summary>
        /// The binary data (optional). Up to 140 bytes in total (UserDataHeader + Data).
        /// </summary>
        public byte[] Data { get; set; }

        /// <inheritdoc />
        public override void Validate()
        {
            base.Validate();

            int totalDataLength = (UserDataHeader?.Length ?? 0) + (Data?.Length ?? 0);

            if (totalDataLength == 0)
            {
                throw new SmsValidationException(SmsValidationErrorCode.BinarySmsEmpty);
            }

            if (totalDataLength > 140)
            {
                throw new SmsValidationException(SmsValidationErrorCode.BinarySmsTooLong);
            }
        }
    }
}
