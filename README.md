# Cellsynt.Sms
.NET Core library for using the Cellsynt SMS API

Please note that you will need an account at https://www.cellsynt.com/en/

Their documentation is found at https://www.cellsynt.com/en/sms/api-integration

# Example usage

```C#
try
{
    // Specify your Cellsynt account information
    var credentials = new CellsyntCredentials("USERNAME", "PASSWORD");
    
    // Create the gateway
    ICellsyntSmsGateway gateway = CellsyntSmsGatewayFactory.Create(credentials);
    
    // Sending an SMS to 0012345678
    SendResult sendResult = await gateway.Send(
        SmsOriginator.AsAlpha("The Sender"),
        new TextSmsMessage("Hello, world!", "0012345678"));

    // The tracking ID's in the send result can be used to verify the delivery
    Console.WriteLine(string.Join(",", sendResult.TrackingIds));
}
catch (Exception exception)
{
    // Something went wrong...
    Console.WriteLine(exception);
}```

