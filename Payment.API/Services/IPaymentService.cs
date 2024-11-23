using Payment.API.Models;

namespace Payment.API.Services
{
    public interface IPaymentService
    {
        Task<PaymentConfirmation> InitiatePaymentAsync(PaymentRequest paymentRequest);
    }
}
