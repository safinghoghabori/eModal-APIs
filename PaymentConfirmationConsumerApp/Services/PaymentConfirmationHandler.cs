using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public class PaymentConfirmationHandler : IPaymentConfirmationHandler
    {
        //private readonly ICosmosDbService _cosmosDbService;
        //private readonly ISqlDbService _sqlDbService;

        //public PaymentConfirmationHandler(ICosmosDbService cosmosDbService, ISqlDbService sqlDbService)
        //{
        //    _cosmosDbService = cosmosDbService;
        //    _sqlDbService = sqlDbService;
        //}

        public async Task HandlePaymentConfirmationAsync(PaymentConfirmation paymentConfirmation)
        {

        }
    }
}
