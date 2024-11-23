using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public interface ISqlDbService
    {
        Task StorePaymentInfoAsync(PaymentConfirmation paymentConfirmation);
    }
}
