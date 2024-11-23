using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public interface ICosmosDbService
    {
        Task UpdateFeesPaidStatusAsync(PaymentConfirmation paymentConfirmation);
    }
}
