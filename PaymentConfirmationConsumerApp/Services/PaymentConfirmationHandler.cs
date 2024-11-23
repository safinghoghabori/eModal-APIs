using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public class PaymentConfirmationHandler : IPaymentConfirmationHandler
    {
        private readonly ISqlDbService _sqlDbService;

        public PaymentConfirmationHandler(ISqlDbService sqlDbService)
        {
            _sqlDbService = sqlDbService;
        }

        public async Task HandlePaymentConfirmationAsync(PaymentConfirmation paymentConfirmation)
        {
            try
            {
                // Store payment information in SQL DB
                await _sqlDbService.StorePaymentInfoAsync(paymentConfirmation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing payment confirmation: {ex.Message}");
            }
        }
    }
}
