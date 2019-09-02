﻿using Cellsynt.Sms.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Cellsynt.Sms
{
    internal class ApiBodyBuilder
    {
        private readonly CellsyntCredentials _credentials;

        public ApiBodyBuilder(CellsyntCredentials credentials)
        {
            _credentials = credentials;
        }

        public string GetBody(SmsOriginator originator, SmsMessage message)
        {
            var parameters = new Dictionary<string, string>
            {
                { "username", _credentials.UserName },
                { "password", _credentials.Password },
                { "charset", "UTF-8" },
                { "destination", string.Join(",", message.Destinations) }
            };

            if (originator != null)
            {
                parameters.Add("originatortype", GetOriginatorType(originator));
                parameters.Add("originator", originator.Value);
            }

            if (message is TextSmsMessage textSmsMessage)
            {
                AppendTextSmsMessageParameters(parameters, textSmsMessage);
            }
            else if (message is BinarySmsMessage binarySmsMessage)
            {
                AppendBinarySmsMessageParameters(parameters, binarySmsMessage);
            }
            else
            {
                throw new SmsValidationException(SmsValidationErrorCode.UnknownType);
            }

            return string.Join("&", parameters.Select(kvp => $"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}"));
        }

        private static string GetOriginatorType(SmsOriginator originator)
        {
            switch (originator.Type)
            {
                case OriginatorType.Numeric:
                    return "numeric";

                case OriginatorType.ShortCode:
                    return "shortcode";

                case OriginatorType.Alpha:
                    return "alpha";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void AppendTextSmsMessageParameters(Dictionary<string, string> parameters, TextSmsMessage message)
        {
            string type;
            switch (message.Encoding)
            {
                case TextSmsEncoding.Gsm0338:
                    type = "text";
                    break;

                case TextSmsEncoding.Unicode:
                    type = "unicode";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            parameters.Add("type", type);

            if (message.MaxMessageCount > 1)
            {
                parameters.Add("allowconcat", message.MaxMessageCount.ToString());
            }

            if (message.Flash)
            {
                parameters.Add("flash", "true");
            }

            parameters.Add("text", message.Text);
        }

        private static void AppendBinarySmsMessageParameters(Dictionary<string, string> parameters, BinarySmsMessage message)
        {
            parameters.Add("type", "binary");

            if ((message.UserDataHeader != null) && (message.UserDataHeader.Length > 0))
            {
                parameters.Add("udh", string.Concat(message.UserDataHeader.Select(x => x.ToString("X2"))));
            }

            if ((message.Data != null) && (message.Data.Length > 0))
            {
                parameters.Add("data", string.Concat(message.Data.Select(x => x.ToString("X2"))));
            }
        }
    }
}