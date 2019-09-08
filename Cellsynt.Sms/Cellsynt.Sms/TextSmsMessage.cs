using Cellsynt.Sms.Exceptions;
using System;

namespace Cellsynt.Sms
{
    /// <summary>
    /// A text SMS message
    /// </summary>
    public class TextSmsMessage : SmsMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="destinations">The message destinations. Each destination should be a numerical string, including country prefix starting with 00, having a max length of 17 digits in total. The maximum number of destinations is 25000.</param>
        public TextSmsMessage(params string[] destinations)
        {
            Destinations.AddRange(destinations);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">The text to send</param>
        /// <param name="destinations">The message destinations. Each destination should be a numerical string, including country prefix starting with 00, having a max length of 17 digits in total. The maximum number of destinations is 25000.</param>
        public TextSmsMessage(string text, params string[] destinations)
        {
            Text = text;
            Destinations.AddRange(destinations);
        }

        /// <summary>
        /// The text to send. A long text may be split into several messages (up to <see cref="MaxMessageCount"/>).
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Encoding of the message
        /// </summary>
        public TextEncodingType EncodingType { get; set; } = TextEncodingType.Unicode;

        /// <summary>
        /// A long text may be split into these many messages. Default = Max = 6.
        /// </summary>
        public int MaxMessageCount { get; set; } = 6;

        /// <summary>
        /// Set to true if the message should be sent as a "flash message", i.e. displayed directly on phone screen instead of being saved to inbox.
        /// Please note that support for flash messages cannot be guaranteed to all operator networks.
        /// If flash is not supported the message will be sent as a regular text message instead.
        /// </summary>
        public bool Flash { get; set; }

        /// <inheritdoc />
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(Text))
            {
                throw new SmsValidationException(SmsValidationErrorCode.TextSmsNoText);
            }

            int charCount;
            int singleMessageCharCount;
            int multiMessageCharCount;

            switch (EncodingType)
            {
                case TextEncodingType.Gsm0338:
                    charCount = Gsm0338Support.GetValidatedCharCount(Text);
                    singleMessageCharCount = 160;
                    multiMessageCharCount = 153;
                    break;

                case TextEncodingType.Unicode:
                    charCount = Text.Length;
                    singleMessageCharCount = 70;
                    multiMessageCharCount = 67;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            int messageCount;
            if (charCount <= singleMessageCharCount)
            {
                messageCount = 1;
            }
            else
            {
                messageCount = (charCount + multiMessageCharCount - 1) / multiMessageCharCount;
            }

            if ((MaxMessageCount < 1) || (MaxMessageCount > 6))
            {
                throw new SmsValidationException(SmsValidationErrorCode.TextSmsTextMaxMessageCountOutOfRange);
            }

            if (messageCount > MaxMessageCount)
            {
                throw new SmsValidationException(SmsValidationErrorCode.TextSmsTextTooLong);
            }
        }
    }
}
