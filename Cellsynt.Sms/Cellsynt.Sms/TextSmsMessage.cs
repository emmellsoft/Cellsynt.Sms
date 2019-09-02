using Cellsynt.Sms.Exceptions;
using System;

namespace Cellsynt.Sms
{
    public class TextSmsMessage : SmsMessage
    {
        public TextSmsMessage()
        {
        }

        public TextSmsMessage(string text, params string[] destinations)
        {
            Text = text;
            Destinations.AddRange(destinations);
        }

        /// <summary>
        /// The text to send
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Encoding of the message
        /// </summary>
        public TextSmsEncoding Encoding { get; set; } = TextSmsEncoding.Unicode;

        /// <summary>
        /// A long text may be split into these many messages. Max = 6.
        /// </summary>
        public int MaxMessageCount { get; set; } = 6;

        /// <summary>
        /// Set to true if the message should be sent as a "flash message", i.e. displayed directly on phone screen instead of being saved to inbox.
        /// Please note that support for flash messages cannot be guaranteed to all operator networks.
        /// If flash is not supported the message will be sent as a regular text message instead.
        /// </summary>
        public bool Flash { get; set; }

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

            switch (Encoding)
            {
                case TextSmsEncoding.Gsm0338:
                    charCount = Gsm0338Support.GetValidatedCharCount(Text);
                    singleMessageCharCount = 160;
                    multiMessageCharCount = 153;
                    break;

                case TextSmsEncoding.Unicode:
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
