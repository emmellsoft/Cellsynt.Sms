using Cellsynt.Sms.Exceptions;
using Shouldly;
using System.Linq;
using Xunit;

namespace Cellsynt.Sms.Test
{
    public class SmsMessageDestinationTests
    {
        private readonly TextSmsMessage _message;

        public SmsMessageDestinationTests()
        {
            _message = new TextSmsMessage("A");
        }

        [Fact]
        public void No_destinations_is_invalid()
        {
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.NoDestinations);
        }

        [Fact]
        public void Destination_1st_null_is_invalid()
        {
            _message.Destinations.Add(null);
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.EmptyDestination);
        }

        [Fact]
        public void Destination_1st_empty_is_invalid()
        {
            _message.Destinations.Add(string.Empty);
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.EmptyDestination);
        }

        [Fact]
        public void One_destination_is_valid()
        {
            _message.Destinations.Add("0099887766");
            _message.Validate();
        }

        [Fact]
        public void One_valid_and_one_empty_destination_is_invalid()
        {
            _message.Destinations.Add("0099887766");
            _message.Destinations.Add(string.Empty);
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.EmptyDestination);
        }

        [Fact]
        public void Destination_missing_00_prefix_is_invalid()
        {
            _message.Destinations.Add("1234");
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.IllegalDestinationPrefix);
        }

        [Fact]
        public void Destination_not_numeric_is_invalid()
        {
            _message.Destinations.Add("0012x67");
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.NonNumericalDestination);
        }

        [Fact]
        public void Destination_max_length_is_valid()
        {
            _message.Destinations.Add("00123456789012345");
            _message.Validate();
        }

        [Fact]
        public void Destination_too_long_is_invalid()
        {
            _message.Destinations.Add("001234567890123456");
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TooLongDestination);
        }

        [Fact]
        public void Destinations_max_count_is_valid()
        {
            _message.Destinations.AddRange(Enumerable.Repeat("001234", 25000));
            _message.Validate();
        }

        [Fact]
        public void Destinations_too_many_is_invalid()
        {
            _message.Destinations.AddRange(Enumerable.Repeat("001234", 25001));
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TooManyDestinations);
        }
    }
}
