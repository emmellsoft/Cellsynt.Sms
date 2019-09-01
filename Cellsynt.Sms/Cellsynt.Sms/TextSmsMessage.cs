using Cellsynt.Sms.Exceptions;
using System;

namespace Cellsynt.Sms
{
    public class TextSmsMessage : SmsMessage
    {
        public TextSmsMessage()
        {
            MaxMessageCount = 1;
        }

        public TextSmsMessage(string destination)
            : base(destination)
        {
            MaxMessageCount = 1;
        }

        /// <summary>
        /// The text to send
        /// </summary>
        public string Text { get; set; }

        public TextSmsEncoding Encoding { get; set; }

        /// <summary>
        /// A long text may be split into these many messages. Max = 6.
        /// </summary>
        public int MaxMessageCount { get; set; }

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

            int maxMessageCount = MaxMessageCount;
            if (maxMessageCount > 6)
            {
                maxMessageCount = 6;
            }

            if (messageCount > maxMessageCount)
            {
                throw new SmsValidationException(SmsValidationErrorCode.TextSmsTextTooLong);
            }
        }
    }
}
