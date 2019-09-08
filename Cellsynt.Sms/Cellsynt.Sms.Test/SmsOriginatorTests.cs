using Cellsynt.Sms.Exceptions;
using Shouldly;
using System;
using Xunit;

namespace Cellsynt.Sms.Test
{
    public class SmsOriginatorTests
    {
        public class Numeric : SmsOriginatorTests
        {
            [Fact]
            public void Null_is_invalid()
            {
                Should.Throw<ArgumentNullException>(() => SmsOriginator.AsNumeric(null));
            }

            [Fact]
            public void Empty_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsNumeric(string.Empty))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorEmpty);
            }

            [Fact]
            public void Leading_zero_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsNumeric("0123"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorLeadingZero);
            }

            [Fact]
            public void One_digit_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsNumeric("1");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Numeric);
                originator.Value.ShouldBe("1");
            }

            [Fact]
            public void Five_digits_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsNumeric("12345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Numeric);
                originator.Value.ShouldBe("12345");
            }

            [Fact]
            public void Fifteen_digits_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsNumeric("123456789012345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Numeric);
                originator.Value.ShouldBe("123456789012345");
            }

            [Fact]
            public void Sixteen_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsNumeric("1234567890123456"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorTooLong);
            }

            [Fact]
            public void Non_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsNumeric("123A45"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorNonNumeric);
            }
        }

        public class ShortCode : SmsOriginatorTests
        {
            [Fact]
            public void Null_is_invalid()
            {
                Should.Throw<ArgumentNullException>(() => SmsOriginator.AsShortCode(null));
            }

            [Fact]
            public void Empty_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsShortCode(string.Empty))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorEmpty);
            }

            [Fact]
            public void Leading_zero_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsShortCode("0123");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("0123");
            }

            [Fact]
            public void One_digit_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsShortCode("1");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("1");
            }

            [Fact]
            public void Five_digits_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsShortCode("12345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("12345");
            }

            [Fact]
            public void Fifteen_digits_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsShortCode("123456789012345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("123456789012345");
            }

            [Fact]
            public void Sixteen_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsShortCode("1234567890123456"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorTooLong);
            }

            [Fact]
            public void Non_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsShortCode("123A45"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorNonNumeric);
            }
        }

        public class Alpha : SmsOriginatorTests
        {
            [Fact]
            public void Null_is_invalid()
            {
                Should.Throw<ArgumentNullException>(() => SmsOriginator.AsAlpha(null));
            }

            [Fact]
            public void Empty_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsAlpha(string.Empty))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorEmpty);
            }

            [Fact]
            public void One_char_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsAlpha("A");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("A");
            }

            [Fact]
            public void Five_chars_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsAlpha("Abc42");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("Abc42");
            }

            [Fact]
            public void Eleven_chars_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsAlpha("AbCZz12h39Q");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("AbCZz12h39Q");
            }

            [Fact]
            public void Twelve_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsAlpha("AbCZz12h39QX"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorTooLong);
            }

            [Fact]
            public void Non_Supported_chars_when_strict_is_invalid()
            {
                Should.Throw<OriginatorException>(() => SmsOriginator.AsAlpha("Café", true))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.OriginatorUnsupportedChars);
            }

            [Fact]
            public void Non_Supported_chars_when_not_strict_is_valid()
            {
                SmsOriginator originator = SmsOriginator.AsAlpha("Café", false);
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("Café");
            }
        }
    }
}
