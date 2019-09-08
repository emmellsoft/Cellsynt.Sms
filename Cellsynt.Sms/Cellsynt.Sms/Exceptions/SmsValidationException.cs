using System;

namespace Cellsynt.Sms.Exceptions
{
    /// <summary>
    /// Invalid SMS message content setup
    /// </summary>
    public class SmsValidationException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">The error code</param>
        public SmsValidationException(SmsValidationErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// The error code
        /// </summary>
        public SmsValidationErrorCode ErrorCode { get; }
    }
}
