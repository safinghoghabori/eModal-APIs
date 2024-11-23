using PaymentConfirmationConsumerApp.Databases;
using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public class SqlDbService: ISqlDbService
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public SqlDbService(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task StorePaymentInfoAsync(PaymentConfirmation paymentConfirmation)
        {
            _applicationDBContext.Add(paymentConfirmation);
            await _applicationDBContext.SaveChangesAsync();
        }
    }
}
