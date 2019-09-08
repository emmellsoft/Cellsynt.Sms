namespace Cellsynt.Sms.Exceptions
{
    /// <summary>
    /// The various SMS validation errors
    /// </summary>
    public enum SmsValidationErrorCode
    {
        /// <summary>
        /// The SmsMessage is of unknown type
        /// </summary>
        UnknownType,

        /// <summary>
        /// No destinations specified
        /// </summary>
        NoDestinations,

        /// <summary>
        /// Too many destinations given (max 25000)
        /// </summary>
        TooManyDestinations,

        /// <summary>
        /// At least one destination in the list is null or empty
        /// </summary>
        EmptyDestination,

        /// <summary>
        /// At least one destination in the list doesn't start with "00"
        /// </summary>
        IllegalDestinationPrefix,

        /// <summary>
        /// At least one destination in the list is too long (max 17 digits)
        /// </summary>
        TooLongDestination,

        /// <summary>
        /// At least one destination in the list has a non-numerical character
        /// </summary>
        NonNumericalDestination,

        /// <summary>
        /// TextSmsMessage: No text is specified
        /// </summary>
        TextSmsNoText,

        /// <summary>
        /// TextSmsMessage: The text contains illegal characters
        /// </summary>
        TextSmsIllegalChar,

        /// <summary>
        /// TextSmsMessage: The MaxMessageCount property must be between 1 and 6.
        /// </summary>
        TextSmsTextMaxMessageCountOutOfRange,

        /// <summary>
        /// TextSmsMessage: The text is too long
        /// </summary>
        TextSmsTextTooLong,

        /// <summary>
        /// BinarySmsMessage: No data to send
        /// </summary>
        BinarySmsEmpty,

        /// <summary>
        /// BinarySmsMessage: No data to send (total byte count = 140)
        /// </summary>
        BinarySmsTooLong,
    }
}
