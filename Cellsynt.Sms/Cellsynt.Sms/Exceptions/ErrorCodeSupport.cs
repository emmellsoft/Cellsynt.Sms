using System;

namespace Cellsynt.Sms.Exceptions
{
    internal static class ErrorCodeSupport
    {
        public static string GetMessage(OriginatorErrorCode errorCode)
        {
            switch (errorCode)
            {
                case OriginatorErrorCode.Empty:
                    return "No originator specified";

                case OriginatorErrorCode.TooLong:
                    return "Too long originator";

                case OriginatorErrorCode.LeadingZero:
                    return "The originator should not have leading zeros";

                case OriginatorErrorCode.NonNumeric:
                    return "The originator is not numeric";

                case OriginatorErrorCode.UnsupportedChars:
                    return "The alpha originator had unsupported characters. Please set the 'strict' flag to false to (try to) use unsupported characters.";

                default:
                    throw new ArgumentOutOfRangeException(nameof(errorCode), errorCode, null);
            }
        }

        public static string GetMessage(SmsValidationErrorCode errorCode)
        {
            switch (errorCode)
            {
                case SmsValidationErrorCode.UnknownType:
                    return "The SmsMessage is of unknown type";

                case SmsValidationErrorCode.NoDestinations:
                    return "No destinations specified";

                case SmsValidationErrorCode.TooManyDestinations:
                    return "Too many destinations given (max 25000)";

                case SmsValidationErrorCode.EmptyDestination:
                    return "At least one destination in the list is null or empty";

                case SmsValidationErrorCode.IllegalDestinationPrefix:
                    return "At least one destination in the list doesn't start with \"00\"";

                case SmsValidationErrorCode.TooLongDestination:
                    return "At least one destination in the list is too long (max 17 digits)";

                case SmsValidationErrorCode.NonNumericalDestination:
                    return "At least one destination in the list has a non-numerical character";

                case SmsValidationErrorCode.TextSmsNoText:
                    return "TextSmsMessage: No text is specified";

                case SmsValidationErrorCode.TextSmsIllegalChar:
                    return "TextSmsMessage: The text contains illegal characters";

                case SmsValidationErrorCode.TextSmsTextMaxMessageCountOutOfRange:
                    return "TextSmsMessage: The MaxMessageCount property must be between 1 and 6.";

                case SmsValidationErrorCode.TextSmsTextTooLong:
                    return "TextSmsMessage: The text is too long";

                case SmsValidationErrorCode.BinarySmsEmpty:
                    return "BinarySmsMessage: No data to send";

                case SmsValidationErrorCode.BinarySmsTooLong:
                    return "BinarySmsMessage: No data to send (total byte count = 140)";

                default:
                    throw new ArgumentOutOfRangeException(nameof(errorCode), errorCode, null);
            }
        }
    }
}
