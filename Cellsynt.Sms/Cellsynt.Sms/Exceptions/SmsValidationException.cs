using System;

namespace Cellsynt.Sms.Exceptions
{
    public class SmsValidationException : Exception
    {
        public SmsValidationException(SmsValidationErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public SmsValidationErrorCode ErrorCode { get; }
    }
}
