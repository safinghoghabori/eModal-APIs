using Payment.API.Models;

namespace Payment.API.Services
{
    public interface IAzureServiceBusClient
    {
        Task SendMessageToQueueAsync(PaymentConfirmation paymentConfirmation);
    }
}
