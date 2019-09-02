namespace Cellsynt.Sms
{
    public class SmsOriginator
    {
        public static SmsOriginator FromNumeric(string originator)
        {
            return new SmsOriginator(OriginatorType.Numeric, OriginatorValidator.ValidateNumeric(originator));
        }

        public static SmsOriginator FromShortCode(string originator)
        {
            return new SmsOriginator(OriginatorType.ShortCode, OriginatorValidator.ValidateShortCode(originator));
        }

        public static SmsOriginator FromAlpha(string originator, bool strict = true)
        {
            return new SmsOriginator(OriginatorType.Alpha, OriginatorValidator.ValidateAlpha(originator, strict));
        }

        private SmsOriginator(OriginatorType type, string value)
        {
            Type = type;
            Value = value;
        }

        public OriginatorType Type { get; }

        public string Value { get; }
    }
}
