namespace Cellsynt.Sms
{
    /// <summary>
    /// The originator for a sent message
    /// </summary>
    public class Originator
    {
        /// <summary>
        /// Create an originator as a phone number
        /// </summary>
        /// <param name="originator">The sending phone number. Include the country code (except the leading 00). Up to 15 digits.</param>
        /// <returns></returns>
        public static Originator AsPhoneNumber(string originator)
        {
            return new Originator(OriginatorType.PhoneNumber, OriginatorValidator.ValidateNumeric(originator));
        }

        /// <summary>
        /// Create an originator as short code
        /// </summary>
        /// <param name="originator">A numerical short code up to 15 digits.</param>
        /// <returns></returns>
        public static Originator AsShortCode(string originator)
        {
            return new Originator(OriginatorType.ShortCode, OriginatorValidator.ValidateShortCode(originator));
        }

        /// <summary>
        /// Create an originator as alpha
        /// </summary>
        /// <param name="originator">A text string representing the sender.
        /// Up to 11 characters. A-Z, a-z and 0-9 are guaranteed to work. Set the 'strict' parameter to false to try go outside this boundary.
        /// The recipient may not reply to this SMS.</param>
        /// <param name="strict">True (default) if only strictly supported characters may be used</param>
        /// <returns></returns>
        public static Originator AsAlpha(string originator, bool strict = true)
        {
            return new Originator(OriginatorType.Alpha, OriginatorValidator.ValidateAlpha(originator, strict));
        }

        private Originator(OriginatorType type, string value)
        {
            Type = type;
            Value = value;
        }

        /// <summary>
        /// The type of originator
        /// </summary>
        public OriginatorType Type { get; }

        /// <summary>
        /// The originator string
        /// </summary>
        public string Value { get; }
    }
}
