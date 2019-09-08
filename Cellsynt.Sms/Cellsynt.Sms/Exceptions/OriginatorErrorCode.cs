namespace Cellsynt.Sms.Exceptions
{
    /// <summary>
    /// The various originator validation errors
    /// </summary>
    public enum OriginatorErrorCode
    {
        /// <summary>
        /// No originator specified
        /// </summary>
        Empty,

        /// <summary>
        /// Too long originator
        /// </summary>
        TooLong,

        /// <summary>
        /// The originator should not have leading zeros
        /// </summary>
        LeadingZero,

        /// <summary>
        /// The originator is not numeric
        /// </summary>
        NonNumeric,

        /// <summary>
        /// The alpha originator had unsupported characters. Please set the 'strict' flag to false to (try to) use unsupported characters.
        /// </summary>
        UnsupportedChars
    }
}
