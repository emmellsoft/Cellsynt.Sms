using Cellsynt.Sms.Exceptions;
using Shouldly;
using System;
using Xunit;

namespace Cellsynt.Sms.Test
{
    public class OriginatorTests
    {
        public class PhoneNumber : OriginatorTests
        {
            [Fact]
            public void Null_is_invalid()
            {
                Should.Throw<ArgumentNullException>(() => Originator.AsPhoneNumber(null));
            }

            [Fact]
            public void Empty_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsPhoneNumber(string.Empty))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.Empty);
            }

            [Fact]
            public void Leading_zero_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsPhoneNumber("0123"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.LeadingZero);
            }

            [Fact]
            public void One_digit_is_valid()
            {
                Originator originator = Originator.AsPhoneNumber("1");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.PhoneNumber);
                originator.Value.ShouldBe("1");
            }

            [Fact]
            public void Five_digits_is_valid()
            {
                Originator originator = Originator.AsPhoneNumber("12345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.PhoneNumber);
                originator.Value.ShouldBe("12345");
            }

            [Fact]
            public void Fifteen_digits_is_valid()
            {
                Originator originator = Originator.AsPhoneNumber("123456789012345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.PhoneNumber);
                originator.Value.ShouldBe("123456789012345");
            }

            [Fact]
            public void Sixteen_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsPhoneNumber("1234567890123456"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.TooLong);
            }

            [Fact]
            public void Non_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsPhoneNumber("123A45"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.NonNumeric);
            }
        }

        public class ShortCode : OriginatorTests
        {
            [Fact]
            public void Null_is_invalid()
            {
                Should.Throw<ArgumentNullException>(() => Originator.AsShortCode(null));
            }

            [Fact]
            public void Empty_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsShortCode(string.Empty))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.Empty);
            }

            [Fact]
            public void Leading_zero_is_valid()
            {
                Originator originator = Originator.AsShortCode("0123");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("0123");
            }

            [Fact]
            public void One_digit_is_valid()
            {
                Originator originator = Originator.AsShortCode("1");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("1");
            }

            [Fact]
            public void Five_digits_is_valid()
            {
                Originator originator = Originator.AsShortCode("12345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("12345");
            }

            [Fact]
            public void Fifteen_digits_is_valid()
            {
                Originator originator = Originator.AsShortCode("123456789012345");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.ShortCode);
                originator.Value.ShouldBe("123456789012345");
            }

            [Fact]
            public void Sixteen_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsShortCode("1234567890123456"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.TooLong);
            }

            [Fact]
            public void Non_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsShortCode("123A45"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.NonNumeric);
            }
        }

        public class Alpha : OriginatorTests
        {
            [Fact]
            public void Null_is_invalid()
            {
                Should.Throw<ArgumentNullException>(() => Originator.AsAlpha(null));
            }

            [Fact]
            public void Empty_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsAlpha(string.Empty))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.Empty);
            }

            [Fact]
            public void One_char_is_valid()
            {
                Originator originator = Originator.AsAlpha("A");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("A");
            }

            [Fact]
            public void Five_chars_is_valid()
            {
                Originator originator = Originator.AsAlpha("Abc42");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("Abc42");
            }

            [Fact]
            public void Eleven_chars_is_valid()
            {
                Originator originator = Originator.AsAlpha("AbCZz12h39Q");
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("AbCZz12h39Q");
            }

            [Fact]
            public void Twelve_digits_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsAlpha("AbCZz12h39QX"))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.TooLong);
            }

            [Fact]
            public void Non_Supported_chars_when_strict_is_invalid()
            {
                Should.Throw<OriginatorException>(() => Originator.AsAlpha("Café", true))
                    .ErrorCode.ShouldBe(OriginatorErrorCode.UnsupportedChars);
            }

            [Fact]
            public void Non_Supported_chars_when_not_strict_is_valid()
            {
                Originator originator = Originator.AsAlpha("Café", false);
                originator.ShouldNotBeNull();
                originator.Type.ShouldBe(OriginatorType.Alpha);
                originator.Value.ShouldBe("Café");
            }
        }
    }
}
