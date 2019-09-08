using Cellsynt.Sms.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Cellsynt.Sms.Test
{
    public class CellsyntSmsGatewayTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly CellsyntCredentials _credentials;
        private readonly TestableCellsyntSmsGateway _gateway;
        private readonly SmsMessage _dummyMessage;
        private const string DummyOkResponse = "OK: 1234";
        private string _fakeResponse = DummyOkResponse;
        private HttpStatusCode _fakeStatusCode = HttpStatusCode.OK;

        public CellsyntSmsGatewayTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _credentials = new CellsyntCredentials("MyName", "MyPassword");
            _gateway = new TestableCellsyntSmsGateway(_credentials, () => (_fakeResponse, _fakeStatusCode));
            _dummyMessage = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };
        }

        [Fact]
        public void Constructing_with_null_Credentials_is_invalid()
        {
            Should.Throw<ArgumentNullException>(() => new TestableCellsyntSmsGateway(null));
        }

        [Fact]
        public void Sending_with_null_message_is_invalid()
        {
            Should.Throw<ArgumentNullException>(() => _gateway.Send(null, null));
        }

        [Fact]
        public async Task Can_alter_credentials_after_construction()
        {
            // ARRANGE
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            _credentials.UserName = "NewName";
            _credentials.Password = "NewPassword";

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
                {
                    { "username", "NewName" },
                    { "password", "NewPassword" },
                    { "destination", "0012345" },
                    { "type", "text" },
                    { "charset", "UTF-8" },
                    { "text", "Hello, World!" }
                });
        }

        [Fact]
        public async Task Text_with_no_originator()
        {
            // ARRANGE
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
                {
                    { "username", "MyName" },
                    { "password", "MyPassword" },
                    { "destination", "0012345" },
                    { "type", "text" },
                    { "charset", "UTF-8" },
                    { "text", "Hello, World!" }
                });
        }

        [Fact]
        public async Task Text_with_Numeric_originator()
        {
            // ARRANGE
            Originator originator = Originator.AsPhoneNumber("8888");
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(originator, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
                {
                    { "username", "MyName" },
                    { "password", "MyPassword" },
                    { "originatortype", "numeric" },
                    { "originator", "8888" },
                    { "destination", "0012345" },
                    { "type", "text" },
                    { "charset", "UTF-8" },
                    { "text", "Hello, World!" }
                });
        }

        [Fact]
        public async Task Text_with_ShortCode_originator()
        {
            // ARRANGE
            Originator originator = Originator.AsShortCode("777");
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(originator, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
                {
                    { "username", "MyName" },
                    { "password", "MyPassword" },
                    { "originatortype", "shortcode" },
                    { "originator", "777" },
                    { "destination", "0012345" },
                    { "type", "text" },
                    { "charset", "UTF-8" },
                    { "text", "Hello, World!" }
                });
        }

        [Fact]
        public async Task Text_with_Alpha_originator()
        {
            // ARRANGE
            Originator originator = Originator.AsAlpha("ABC");
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(originator, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
                {
                    { "username", "MyName" },
                    { "password", "MyPassword" },
                    { "originatortype", "alpha" },
                    { "originator", "ABC" },
                    { "destination", "0012345" },
                    { "type", "text" },
                    { "charset", "UTF-8" },
                    { "text", "Hello, World!" }
                });
        }

        [Fact]
        public async Task Text_with_encoding_GSM()
        {
            // ARRANGE
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345" },
                { "type", "text" },
                { "charset", "UTF-8" },
                { "text", "Hello, World!" }
            });
        }

        [Fact]
        public async Task Text_with_encoding_Unicode()
        {
            // ARRANGE
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Unicode
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345" },
                { "type", "unicode" },
                { "charset", "UTF-8" },
                { "text", "Hello, World!" }
            });
        }

        [Fact]
        public async Task Text_with_two_destinations()
        {
            // ARRANGE
            var message = new TextSmsMessage("Hello, World!", "0012345", "0023456")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345,0023456" },
                { "type", "text" },
                { "charset", "UTF-8" },
                { "text", "Hello, World!" }
            });
        }

        [Fact]
        public async Task Text_with_ten_destinations()
        {
            // ARRANGE
            var message = new TextSmsMessage(
                "Hello, World!",
                "0012345",
                "0023456",
                "0034567",
                "0045678",
                "0056789",
                "0067890",
                "0078901",
                "0089012",
                "0090123",
                "0099999")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345,0023456,0034567,0045678,0056789,0067890,0078901,0089012,0090123,0099999" },
                { "type", "text" },
                { "charset", "UTF-8" },
                { "text", "Hello, World!" }
            });
        }

        [Fact]
        public async Task Text_with_allowing_3_messages()
        {
            // ARRANGE
            var message = new TextSmsMessage("Hello, World!", "0012345")
            {
                MaxMessageCount = 3,
                EncodingType = TextEncodingType.Gsm0338
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345" },
                { "type", "text" },
                { "charset", "UTF-8" },
                { "allowconcat", "3" },
                { "text", "Hello, World!" }
            });
        }

        [Fact]
        public async Task Text_with_Greek_unicode_chars()
        {
            // ARRANGE
            var message = new TextSmsMessage("Ελλάδα", "0012345")
            {
                MaxMessageCount = 1,
                EncodingType = TextEncodingType.Unicode
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345" },
                { "type", "unicode" },
                { "charset", "UTF-8" },
                { "text", "Ελλάδα" }
            });
        }

        [Fact]
        public async Task Binary_with_both_UserDataHeader_and_Data()
        {
            // ARRANGE
            var message = new BinarySmsMessage(
                new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 },
                new byte[] { 12, 34, 56, 78, 90 },
                "0012345");

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345" },
                { "type", "binary" },
                { "udh", "0A141E28323C4650" },
                { "data", "0C22384E5A" }
            });
        }

        [Fact]
        public async Task Binary_with_only_UserDataHeader()
        {
            // ARRANGE
            var message = new BinarySmsMessage("0012345")
            {
                UserDataHeader = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 }
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345" },
                { "type", "binary" },
                { "udh", "0A141E28323C4650" }
            });
        }

        [Fact]
        public async Task Binary_with_only_Data()
        {
            // ARRANGE
            var message = new BinarySmsMessage("0012345")
            {
                Data = new byte[] { 12, 34, 56, 78, 90 }
            };

            // ACT
            await _gateway.Send(null, message);

            // ASSERT
            AssertBody(new Dictionary<string, string>
            {
                { "username", "MyName" },
                { "password", "MyPassword" },
                { "destination", "0012345" },
                { "type", "binary" },
                { "data", "0C22384E5A" }
            });
        }

        [Fact]
        public async Task Can_parse_tracking_ID()
        {
            // ARRANGE
            _fakeResponse = "OK: 123456789";

            // ACT
            SendResult sendResult = await _gateway.Send(null, _dummyMessage);

            // ASSERT
            sendResult.TrackingIds.ShouldBe(new[] { "123456789" });
        }

        [Fact]
        public async Task Can_parse_multiple_tracking_IDs()
        {
            // ARRANGE
            _fakeResponse = "OK: 123456789,1122334455,6677889900";

            // ACT
            SendResult sendResult = await _gateway.Send(null, _dummyMessage);

            // ASSERT
            sendResult.TrackingIds.ShouldBe(new[] { "123456789", "1122334455", "6677889900" });
        }

        [Fact]
        public async Task Can_parse_error_code()
        {
            // ARRANGE
            _fakeResponse = "Error: An error message from the host";
            _fakeStatusCode = HttpStatusCode.BadRequest;

            // ACT
            SmsSendException exception = await Should.ThrowAsync<SmsSendException>(() => _gateway.Send(null, _dummyMessage));

            // ASSERT
            exception.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            exception.Message.ShouldBe("An error message from the host");
        }

        [Fact]
        public async Task Can_parse_out_of_credits_error()
        {
            // ARRANGE
            _fakeResponse = "Error: Out of credits";
            _fakeStatusCode = HttpStatusCode.BadRequest;

            // ACT
            await Should.ThrowAsync<OutOfCreditsException>(() => _gateway.Send(null, _dummyMessage));
        }

        private void AssertBody(IDictionary<string, string> expectedBody)
        {
            _outputHelper.WriteLine("Parameters:");
            foreach (var kvp in _gateway.SentParameters)
            {
                _outputHelper.WriteLine($"{kvp.Key}: \"{kvp.Value}\"");
            }

            _gateway.SentParameters.Count.ShouldBe(expectedBody.Count);
            foreach (var expectedKvp in expectedBody)
            {
                _gateway.SentParameters.TryGetValue(expectedKvp.Key, out string actualValue).ShouldBeTrue();
                actualValue.ShouldBe(expectedKvp.Value);
            }
        }

        private class TestableCellsyntSmsGateway : CellsyntSmsGateway
        {
            private readonly Func<(string, HttpStatusCode)> _getResponse;

            public TestableCellsyntSmsGateway(
                CellsyntCredentials credentials,
                Func<(string, HttpStatusCode)> getResponse = null)
                : base(credentials)
            {
                _getResponse = getResponse ?? (() => (DummyOkResponse, HttpStatusCode.OK));
            }

            public IDictionary<string, string> SentParameters { get; private set; }

            protected override Task<(string, HttpStatusCode)> Send(IDictionary<string, string> parameters)
            {
                SentParameters = parameters;
                return Task.FromResult(_getResponse());
            }
        }
    }
}
