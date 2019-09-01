namespace Cellsynt.Sms.Exceptions
{
    public enum OriginatorErrorCode
    {
        OriginatorEmpty,
        OriginatorTooLong,
        OriginatorLeadingZero,
        OriginatorNonNumeric,
        OriginatorUnsupportedChars
    }
}
