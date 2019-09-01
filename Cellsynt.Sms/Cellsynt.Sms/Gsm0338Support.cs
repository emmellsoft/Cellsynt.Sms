using Cellsynt.Sms.Exceptions;
using System.Linq;

namespace Cellsynt.Sms
{
    internal static class Gsm0338Support
    {
        private static readonly char[] ValidChars;
        private static readonly char[] DoubleChars;

        static Gsm0338Support()
        {
            DoubleChars = new[]
            {
                '\u000C', // FORM FEED
                '\u005E', // CIRCUMFLEX ACCENT
                '\u007B', // LEFT CURLY BRACKET
                '\u007D', // RIGHT CURLY BRACKET
                '\u005C', // REVERSE SOLIDUS
                '\u005B', // LEFT SQUARE BRACKET
                '\u007E', // TILDE
                '\u005D', // RIGHT SQUARE BRACKET
                '\u007C', // VERTICAL LINE
                '\u20AC'  // EURO SIGN
            };

            ValidChars = new[]
            {
                '\u0040', // COMMERCIAL AT
                '\u0000', // NULL (see note above)
                '\u00A3', // POUND SIGN
                '\u0024', // DOLLAR SIGN
                '\u00A5', // YEN SIGN
                '\u00E8', // LATIN SMALL LETTER E WITH GRAVE
                '\u00E9', // LATIN SMALL LETTER E WITH ACUTE
                '\u00F9', // LATIN SMALL LETTER U WITH GRAVE
                '\u00EC', // LATIN SMALL LETTER I WITH GRAVE
                '\u00F2', // LATIN SMALL LETTER O WITH GRAVE
                '\u00E7', // LATIN SMALL LETTER C WITH CEDILLA
                '\u00C7', // LATIN CAPITAL LETTER C WITH CEDILLA (see note above)
                '\u000A', // LINE FEED
                '\u00D8', // LATIN CAPITAL LETTER O WITH STROKE
                '\u00F8', // LATIN SMALL LETTER O WITH STROKE
                '\u000D', // CARRIAGE RETURN
                '\u00C5', // LATIN CAPITAL LETTER A WITH RING ABOVE
                '\u00E5', // LATIN SMALL LETTER A WITH RING ABOVE
                '\u0394', // GREEK CAPITAL LETTER DELTA
                '\u005F', // LOW LINE
                '\u03A6', // GREEK CAPITAL LETTER PHI
                '\u0393', // GREEK CAPITAL LETTER GAMMA
                '\u039B', // GREEK CAPITAL LETTER LAMDA
                '\u03A9', // GREEK CAPITAL LETTER OMEGA
                '\u03A0', // GREEK CAPITAL LETTER PI
                '\u03A8', // GREEK CAPITAL LETTER PSI
                '\u03A3', // GREEK CAPITAL LETTER SIGMA
                '\u0398', // GREEK CAPITAL LETTER THETA
                '\u039E', // GREEK CAPITAL LETTER XI
                '\u00A0', // ESCAPE TO EXTENSION TABLE (or displayed as NBSP, see note above)
                '\u000C', // FORM FEED
                '\u005E', // CIRCUMFLEX ACCENT
                '\u007B', // LEFT CURLY BRACKET
                '\u007D', // RIGHT CURLY BRACKET
                '\u005C', // REVERSE SOLIDUS
                '\u005B', // LEFT SQUARE BRACKET
                '\u007E', // TILDE
                '\u005D', // RIGHT SQUARE BRACKET
                '\u007C', // VERTICAL LINE
                '\u20AC', // EURO SIGN
                '\u00C6', // LATIN CAPITAL LETTER AE
                '\u00E6', // LATIN SMALL LETTER AE
                '\u00DF', // LATIN SMALL LETTER SHARP S (German)
                '\u00C9', // LATIN CAPITAL LETTER E WITH ACUTE
                '\u0020', // SPACE
                '\u0021', // EXCLAMATION MARK
                '\u0022', // QUOTATION MARK
                '\u0023', // NUMBER SIGN
                '\u00A4', // CURRENCY SIGN
                '\u0025', // PERCENT SIGN
                '\u0026', // AMPERSAND
                '\u0027', // APOSTROPHE
                '\u0028', // LEFT PARENTHESIS
                '\u0029', // RIGHT PARENTHESIS
                '\u002A', // ASTERISK
                '\u002B', // PLUS SIGN
                '\u002C', // COMMA
                '\u002D', // HYPHEN-MINUS
                '\u002E', // FULL STOP
                '\u002F', // SOLIDUS
                '\u0030', // DIGIT ZERO
                '\u0031', // DIGIT ONE
                '\u0032', // DIGIT TWO
                '\u0033', // DIGIT THREE
                '\u0034', // DIGIT FOUR
                '\u0035', // DIGIT FIVE
                '\u0036', // DIGIT SIX
                '\u0037', // DIGIT SEVEN
                '\u0038', // DIGIT EIGHT
                '\u0039', // DIGIT NINE
                '\u003A', // COLON
                '\u003B', // SEMICOLON
                '\u003C', // LESS-THAN SIGN
                '\u003D', // EQUALS SIGN
                '\u003E', // GREATER-THAN SIGN
                '\u003F', // QUESTION MARK
                '\u00A1', // INVERTED EXCLAMATION MARK
                '\u0041', // LATIN CAPITAL LETTER A
                '\u0391', // GREEK CAPITAL LETTER ALPHA
                '\u0042', // LATIN CAPITAL LETTER B
                '\u0392', // GREEK CAPITAL LETTER BETA
                '\u0043', // LATIN CAPITAL LETTER C
                '\u0044', // LATIN CAPITAL LETTER D
                '\u0045', // LATIN CAPITAL LETTER E
                '\u0395', // GREEK CAPITAL LETTER EPSILON
                '\u0046', // LATIN CAPITAL LETTER F
                '\u0047', // LATIN CAPITAL LETTER G
                '\u0048', // LATIN CAPITAL LETTER H
                '\u0397', // GREEK CAPITAL LETTER ETA
                '\u0049', // LATIN CAPITAL LETTER I
                '\u0399', // GREEK CAPITAL LETTER IOTA
                '\u004A', // LATIN CAPITAL LETTER J
                '\u004B', // LATIN CAPITAL LETTER K
                '\u039A', // GREEK CAPITAL LETTER KAPPA
                '\u004C', // LATIN CAPITAL LETTER L
                '\u004D', // LATIN CAPITAL LETTER M
                '\u039C', // GREEK CAPITAL LETTER MU
                '\u004E', // LATIN CAPITAL LETTER N
                '\u039D', // GREEK CAPITAL LETTER NU
                '\u004F', // LATIN CAPITAL LETTER O
                '\u039F', // GREEK CAPITAL LETTER OMICRON
                '\u0050', // LATIN CAPITAL LETTER P
                '\u03A1', // GREEK CAPITAL LETTER RHO
                '\u0051', // LATIN CAPITAL LETTER Q
                '\u0052', // LATIN CAPITAL LETTER R
                '\u0053', // LATIN CAPITAL LETTER S
                '\u0054', // LATIN CAPITAL LETTER T
                '\u03A4', // GREEK CAPITAL LETTER TAU
                '\u0055', // LATIN CAPITAL LETTER U
                '\u0056', // LATIN CAPITAL LETTER V
                '\u0057', // LATIN CAPITAL LETTER W
                '\u0058', // LATIN CAPITAL LETTER X
                '\u03A7', // GREEK CAPITAL LETTER CHI
                '\u0059', // LATIN CAPITAL LETTER Y
                '\u03A5', // GREEK CAPITAL LETTER UPSILON
                '\u005A', // LATIN CAPITAL LETTER Z
                '\u0396', // GREEK CAPITAL LETTER ZETA
                '\u00C4', // LATIN CAPITAL LETTER A WITH DIAERESIS
                '\u00D6', // LATIN CAPITAL LETTER O WITH DIAERESIS
                '\u00D1', // LATIN CAPITAL LETTER N WITH TILDE
                '\u00DC', // LATIN CAPITAL LETTER U WITH DIAERESIS
                '\u00A7', // SECTION SIGN
                '\u00BF', // INVERTED QUESTION MARK
                '\u0061', // LATIN SMALL LETTER A
                '\u0062', // LATIN SMALL LETTER B
                '\u0063', // LATIN SMALL LETTER C
                '\u0064', // LATIN SMALL LETTER D
                '\u0065', // LATIN SMALL LETTER E
                '\u0066', // LATIN SMALL LETTER F
                '\u0067', // LATIN SMALL LETTER G
                '\u0068', // LATIN SMALL LETTER H
                '\u0069', // LATIN SMALL LETTER I
                '\u006A', // LATIN SMALL LETTER J
                '\u006B', // LATIN SMALL LETTER K
                '\u006C', // LATIN SMALL LETTER L
                '\u006D', // LATIN SMALL LETTER M
                '\u006E', // LATIN SMALL LETTER N
                '\u006F', // LATIN SMALL LETTER O
                '\u0070', // LATIN SMALL LETTER P
                '\u0071', // LATIN SMALL LETTER Q
                '\u0072', // LATIN SMALL LETTER R
                '\u0073', // LATIN SMALL LETTER S
                '\u0074', // LATIN SMALL LETTER T
                '\u0075', // LATIN SMALL LETTER U
                '\u0076', // LATIN SMALL LETTER V
                '\u0077', // LATIN SMALL LETTER W
                '\u0078', // LATIN SMALL LETTER X
                '\u0079', // LATIN SMALL LETTER Y
                '\u007A', // LATIN SMALL LETTER Z
                '\u00E4', // LATIN SMALL LETTER A WITH DIAERESIS
                '\u00F6', // LATIN SMALL LETTER O WITH DIAERESIS
                '\u00F1', // LATIN SMALL LETTER N WITH TILDE
                '\u00FC', // LATIN SMALL LETTER U WITH DIAERESIS
                '\u00E0'  // LATIN SMALL LETTER A WITH GRAVE
            };
        }

        public static int GetValidatedCharCount(string text)
        {
            int charCount = text.Length;
            foreach (char c in text)
            {
                if (!ValidChars.Contains(c))
                {
                    throw new SmsValidationException(SmsValidationErrorCode.TextSmsIllegalChar);
                }

                if (DoubleChars.Contains(c))
                {
                    charCount++;
                }
            }

            return charCount;
        }
    }
}
