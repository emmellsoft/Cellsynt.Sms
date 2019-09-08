using Cellsynt.Sms.Exceptions;
using Shouldly;
using Xunit;

namespace Cellsynt.Sms.Test
{
    public class BinarySmsMessageTests
    {
        private readonly BinarySmsMessage _message;

        public BinarySmsMessageTests()
        {
            _message = new BinarySmsMessage
            {
                Destinations = { "00123" }
            };
        }

        [Fact]
        public void Both_UserDataHeader_and_Data_are_null_is_invalid()
        {
            _message.UserDataHeader = null;
            _message.Data = null;

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsEmpty);
        }

        [Fact]
        public void Both_UserDataHeader_and_Data_are_empty_is_invalid()
        {
            _message.UserDataHeader = new byte[0];
            _message.Data = new byte[0];

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsEmpty);
        }

        [Fact]
        public void UserDataHeader_one_byte_and_Data_is_null_is_valid()
        {
            _message.UserDataHeader = new byte[1];
            _message.Data = null;

            _message.Validate();
        }

        [Fact]
        public void UserDataHeader_140_bytes_and_Data_is_null_is_valid()
        {
            _message.UserDataHeader = new byte[140];
            _message.Data = null;

            _message.Validate();
        }

        [Fact]
        public void UserDataHeader_141_bytes_and_Data_is_null_is_invalid()
        {
            _message.UserDataHeader = new byte[141];
            _message.Data = null;

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsTooLong);
        }

        [Fact]
        public void UserDataHeader_is_null_and_Data_one_byte_is_valid()
        {
            _message.UserDataHeader = null;
            _message.Data = new byte[1];

            _message.Validate();
        }

        [Fact]
        public void UserDataHeader_is_null_and_Data_140_bytes_is_valid()
        {
            _message.UserDataHeader = null;
            _message.Data = new byte[140];

            _message.Validate();
        }

        [Fact]
        public void UserDataHeader_is_null_and_Data_141_bytes_is_invalid()
        {
            _message.UserDataHeader = null;
            _message.Data = new byte[141];

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsTooLong);
        }

        [Fact]
        public void UserDataHeader_70_bytes_and_Data_70_bytes_is_valid()
        {
            _message.UserDataHeader = new byte[70];
            _message.Data = new byte[70];

            _message.Validate();
        }

        [Fact]
        public void UserDataHeader_70_bytes_and_Data_71_bytes_is_invalid()
        {
            _message.UserDataHeader = new byte[70];
            _message.Data = new byte[71];

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsTooLong);
        }

        [Fact]
        public void UserDataHeader_71_bytes_and_Data_70_bytes_is_invalid()
        {
            _message.UserDataHeader = new byte[71];
            _message.Data = new byte[70];

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsTooLong);
        }

        [Fact]
        public void UserDataHeader_280_bytes_and_Data_140_bytes_is_invalid()
        {
            _message.UserDataHeader = new byte[280];
            _message.Data = new byte[140];

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsTooLong);
        }

        [Fact]
        public void UserDataHeader_280_bytes_and_Data_141_bytes_is_invalid()
        {
            _message.UserDataHeader = new byte[280];
            _message.Data = new byte[141];

            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.BinarySmsTooLong);
        }
    }
}
