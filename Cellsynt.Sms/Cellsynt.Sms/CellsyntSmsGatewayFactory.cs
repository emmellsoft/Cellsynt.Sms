using System;

namespace Cellsynt.Sms
{
    /// <summary>
    /// Factory for the SMS gateway
    /// </summary>
    public static class CellsyntSmsGatewayFactory
    {
        /// <summary>
        /// Create an instance to the <see cref="ICellsyntSmsGateway"/>
        /// </summary>
        /// <param name="credentials">The Cellsynt account credentials</param>
        /// <returns>ICellsyntSmsGateway</returns>
        public static ICellsyntSmsGateway Create(CellsyntCredentials credentials)
        {
            return new CellsyntSmsGateway(credentials ?? throw new ArgumentNullException(nameof(credentials)));
        }
    }
}
