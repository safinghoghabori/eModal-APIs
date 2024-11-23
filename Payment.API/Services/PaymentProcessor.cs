using Payment.API.Models;

namespace Payment.API.Services
{
    public class PaymentProcessor: IPaymentProcessor
    {
        public async Task<PaymentConfirmation> ProcessPaymentAsync(PaymentRequest paymentRequest)
        {
            // Simulating payment validation
            if (string.IsNullOrEmpty(paymentRequest.UserId) || paymentRequest.Amount <= 0)
            {
                throw new InvalidOperationException("Invalid payment details.");
            }

            // Simulating payment processing time
            await Task.Delay(1000); 

            // Simulate successful payment
            return new PaymentConfirmation
            {
                UserId = paymentRequest.UserId,
                EdiFileId = paymentRequest.EdiFileId,
                Amount = paymentRequest.Amount,
                IsSuccessful = true, 
                TransactionId = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.UtcNow
            };
        }

    }
}
