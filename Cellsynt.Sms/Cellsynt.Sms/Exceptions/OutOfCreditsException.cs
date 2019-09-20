using System;

namespace Cellsynt.Sms.Exceptions
{
    /// <summary>
    /// Your Cellsynt account is out of credits
    /// </summary>
    public class OutOfCreditsException : Exception
    {
        public OutOfCreditsException()
            : base("You have run out of credits")
        {
        }
    }
}
