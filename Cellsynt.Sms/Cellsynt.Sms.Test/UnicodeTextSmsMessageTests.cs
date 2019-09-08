using Cellsynt.Sms.Exceptions;
using Shouldly;
using Xunit;

namespace Cellsynt.Sms.Test
{
    public class UnicodeTextSmsMessageTests
    {
        private readonly TextSmsMessage _message;

        public UnicodeTextSmsMessageTests()
        {
            _message = new TextSmsMessage(string.Empty, "00123")
            {
                Encoding = TextSmsEncoding.Unicode
            };
        }

        [Fact]
        public void Text_null_is_invalid()
        {
            _message.Text = null;
            _message.MaxMessageCount = 1;
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TextSmsNoText);
        }

        [Fact]
        public void Text_empty_is_invalid()
        {
            _message.Text = string.Empty;
            _message.MaxMessageCount = 1;
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TextSmsNoText);
        }

        [Fact]
        public void Text_single_message_1_char_is_valid()
        {
            _message.Text = "A";
            _message.MaxMessageCount = 1;
            _message.Validate();
        }

        [Fact]
        public void Text_single_message_written_in_Greek_valid()
        {
            _message.Text = "\u0388\u03BD\u03B1 \u03BC\u03AE\u03BD\u03C5\u03BC\u03B1 \u03B3\u03C1\u03B1\u03BC\u03BC\u03AD\u03BD\u03BF \u03C3\u03C4\u03B1 \u03B5\u03BB\u03BB\u03B7\u03BD\u03B9\u03BA\u03AC.";
            _message.MaxMessageCount = 1;
            _message.Validate();
        }

        [Fact]
        public void Text_single_message_70_chars_is_valid()
        {
            _message.Text = new string('A', 70);
            _message.MaxMessageCount = 1;
            _message.Validate();
        }

        [Fact]
        public void Text_4_messages_70_chars_is_valid()
        {
            _message.Text = new string('A', 70);
            _message.MaxMessageCount = 4;
            _message.Validate();
        }

        [Fact]
        public void Text_single_message_71_chars_is_invalid()
        {
            _message.Text = new string('A', 71);
            _message.MaxMessageCount = 1;
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TextSmsTextTooLong);
        }

        [Fact]
        public void Text_2_messages_71_chars_is_valid()
        {
            _message.Text = new string('A', 71);
            _message.MaxMessageCount = 2;
            _message.Validate();
        }

        [Fact]
        public void Text_4_full_messages_is_valid()
        {
            _message.Text = new string('A', 67 * 4);
            _message.MaxMessageCount = 4;
            _message.Validate();
        }

        [Fact]
        public void Text_4_full_messages_plus_och_char_is_invalid()
        {
            _message.Text = new string('A', 67 * 4 + 1);
            _message.MaxMessageCount = 4;
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TextSmsTextTooLong);
        }

        [Fact]
        public void Text_6_full_messages_is_valid()
        {
            _message.Text = new string('A', 67 * 6);
            _message.MaxMessageCount = 6;
            _message.Validate();
        }

        [Fact]
        public void Text_6_full_messages_plus_och_char_is_invalid()
        {
            _message.Text = new string('A', 67 * 6 + 1);
            _message.MaxMessageCount = 6;
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TextSmsTextTooLong);
        }

        [Fact]
        public void Text_7_messages_is_invalid()
        {
            _message.Text = "A";
            _message.MaxMessageCount = 7;
            Should.Throw<SmsValidationException>(() => _message.Validate())
                .ErrorCode.ShouldBe(SmsValidationErrorCode.TextSmsTextMaxMessageCountOutOfRange);
        }

        [Fact]
        public void Text_flash_single_message_1_char_is_valid()
        {
            _message.Text = "A";
            _message.MaxMessageCount = 1;
            _message.Flash = true;
            _message.Validate();
        }
    }
}
