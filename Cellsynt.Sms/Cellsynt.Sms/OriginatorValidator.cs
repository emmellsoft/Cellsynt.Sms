using Cellsynt.Sms.Exceptions;
using System;
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
            if (originator == null)
            {
                throw new ArgumentNullException(nameof(originator));
            }

            if (originator.Length == 0)
            {
                throw new OriginatorException(OriginatorErrorCode.Empty);
            }

            if (originator.Length > 15)
            {
                throw new OriginatorException(OriginatorErrorCode.TooLong);
            }

            if (originator[0] == '0')
            {
                throw new OriginatorException(OriginatorErrorCode.LeadingZero);
            }

            if (originator.Any(c => (c < '0') || (c > '9')))
            {
                throw new OriginatorException(OriginatorErrorCode.NonNumeric);
            }

            return originator;
        }

        public static string ValidateShortCode(string originator)
        {
            if (originator == null)
            {
                throw new ArgumentNullException(nameof(originator));
            }

            if (originator.Length == 0)
            {
                throw new OriginatorException(OriginatorErrorCode.Empty);
            }

            if (originator.Length > 15)
            {
                throw new OriginatorException(OriginatorErrorCode.TooLong);
            }

            if (originator.Any(c => (c < '0') || (c > '9')))
            {
                throw new OriginatorException(OriginatorErrorCode.NonNumeric);
            }

            return originator;
        }

        public static string ValidateAlpha(string originator, bool strict)
        {
            if (originator == null)
            {
                throw new ArgumentNullException(nameof(originator));
            }

            if (originator.Length == 0)
            {
                throw new OriginatorException(OriginatorErrorCode.Empty);
            }

            if (originator.Length > 11)
            {
                throw new OriginatorException(OriginatorErrorCode.TooLong);
            }

            if (strict && originator.Any(c => !ValidAlphaChars.Contains(c)))
            {
                throw new OriginatorException(OriginatorErrorCode.UnsupportedChars);
            }

            return originator;
        }
    }
}
