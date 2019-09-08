using System;

namespace Cellsynt.Sms.Exceptions
{
    /// <summary>
    /// Invalid originator
    /// </summary>
    public class OriginatorException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">The error code</param>
        public OriginatorException(OriginatorErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// The error code
        /// </summary>
        public OriginatorErrorCode ErrorCode { get; }
    }
}
