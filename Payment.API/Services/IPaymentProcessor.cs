using Payment.API.Models;

namespace Payment.API.Services
{
    public interface IPaymentProcessor
    {
        Task<PaymentConfirmation> ProcessPaymentAsync(PaymentRequest paymentRequest);
    }
}
