using System;

namespace Cellsynt.Sms.Exceptions
{
    public class OriginatorException : Exception
    {
        public OriginatorException(OriginatorErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public OriginatorErrorCode ErrorCode { get; }
    }
}
