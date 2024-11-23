using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public interface IPaymentConfirmationHandler
    {
        Task HandlePaymentConfirmationAsync(PaymentConfirmation paymentConfirmation);
    }
}
