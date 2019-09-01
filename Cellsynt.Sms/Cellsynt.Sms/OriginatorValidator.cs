using Cellsynt.Sms.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Cellsynt.Sms
{
    internal static class OriginatorValidator
    {
        private static readonly HashSet<char> ValidAlphaChars;

        static OriginatorValidator()
        {
            ValidAlphaChars = new HashSet<char>(
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "0123456789");
        }

        public static string ValidateNumeric(string originator)
        {
            if (string.IsNullOrEmpty(originator))
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorEmpty);
            }

            if (originator.Length > 15)
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorTooLong);
            }

            if (originator[0] == '0')
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorLeadingZero);
            }

            if (originator.Any(c => (c < '0') || (c > '9')))
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorNonNumeric);
            }

            return originator;
        }

        public static string ValidateShortCode(string originator)
        {
            if (string.IsNullOrEmpty(originator))
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorEmpty);
            }

            if (originator.Length > 15)
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorTooLong);
            }

            if (originator.Any(c => (c < '0') || (c > '9')))
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorNonNumeric);
            }

            return originator;
        }

        public static string ValidateAlpha(string originator, bool strict)
        {
            if (string.IsNullOrEmpty(originator))
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorEmpty);
            }

            if (originator.Length > 11)
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorTooLong);
            }

            if (strict && originator.Any(c => !ValidAlphaChars.Contains(c)))
            {
                throw new OriginatorException(OriginatorErrorCode.OriginatorUnsupportedChars);
            }

            return originator;
        }
    }
}
