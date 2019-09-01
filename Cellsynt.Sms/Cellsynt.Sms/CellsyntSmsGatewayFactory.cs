namespace Cellsynt.Sms
{
    public static class CellsyntSmsGatewayFactory
    {
        public static ICellsyntSmsGateway FromNumericOriginator(string originator)
        {
            return new CellsyntSmsGateway(OriginatorKind.Numeric, OriginatorValidator.ValidateNumeric(originator));
        }

        public static ICellsyntSmsGateway FromShortCodeOriginator(string originator)
        {
            return new CellsyntSmsGateway(OriginatorKind.ShortCode, OriginatorValidator.ValidateShortCode(originator));
        }

        public static ICellsyntSmsGateway FromAlphaOriginator(string originator, bool strict = true)
        {
            return new CellsyntSmsGateway(OriginatorKind.Alpha, OriginatorValidator.ValidateAlpha(originator, strict));
        }
    }
}
