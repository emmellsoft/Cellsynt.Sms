using System;

namespace Cellsynt.Sms
{
    public static class CellsyntSmsGatewayFactory
    {
        public static ICellsyntSmsGateway Create(CellsyntCredentials credentials)
        {
            return new CellsyntSmsGateway(credentials ?? throw new ArgumentNullException(nameof(credentials)));
        }
    }
}
