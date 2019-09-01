﻿using Cellsynt.Sms.Exceptions;

namespace Cellsynt.Sms
{
    public class BinarySmsMessage : SmsMessage
    {
        public BinarySmsMessage()
        {
        }

        public BinarySmsMessage(string destination)
            : base(destination)
        {
        }

        public byte[] UserDataHeader { get; set; }

        public byte[] Data { get; set; }

        public override void Validate()
        {
            base.Validate();

            int totalDataLength = (UserDataHeader?.Length ?? 0) + (Data?.Length ?? 0);

            if (totalDataLength == 0)
            {
                throw new SmsValidationException(SmsValidationErrorCode.BinarySmsEmpty);
            }

            if (totalDataLength > 140)
            {
                throw new SmsValidationException(SmsValidationErrorCode.BinarySmsTooLong);
            }
        }
    }
}