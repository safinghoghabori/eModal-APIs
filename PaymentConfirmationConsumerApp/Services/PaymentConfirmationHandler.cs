using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public class PaymentConfirmationHandler : IPaymentConfirmationHandler
    {
        private readonly ISqlDbService _sqlDbService;
        private readonly ICosmosDbService _cosmosDbService;

        public PaymentConfirmationHandler(ISqlDbService sqlDbService, ICosmosDbService cosmosDbService)
        {
            _sqlDbService = sqlDbService;
            _cosmosDbService = cosmosDbService;
        }

        public async Task HandlePaymentConfirmationAsync(PaymentConfirmation paymentConfirmation)
        {
            try
            {
                // Store payment information in SQL DB
                await _sqlDbService.StorePaymentInfoAsync(paymentConfirmation);
                
                // Update fees status in Cosmos DB
                await _cosmosDbService.UpdateFeesPaidStatusAsync(paymentConfirmation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing payment confirmation: {ex.Message}");
            }
        }
    }
}
