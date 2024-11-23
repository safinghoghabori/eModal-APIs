namespace PaymentConfirmationConsumerApp.Services
{
    public interface IAzureServiceBusClient
    {
        Task ReceiveMessagesAsync();
    }
}
