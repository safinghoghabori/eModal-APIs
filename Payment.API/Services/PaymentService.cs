using Payment.API.Models;

namespace Payment.API.Services
{
    public class PaymentService: IPaymentService
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IAzureServiceBusClient _azureServiceBusClient;

        public PaymentService(IPaymentProcessor paymentProcessor, IAzureServiceBusClient azureServiceBusClient)
        {
            _paymentProcessor = paymentProcessor;
            _azureServiceBusClient = azureServiceBusClient;
        }

        public async Task<PaymentConfirmation> InitiatePaymentAsync(PaymentRequest paymentRequest)
        {
            try
            {
                // Process the payment
                var paymentConfirmation = await _paymentProcessor.ProcessPaymentAsync(paymentRequest);

                // Send payment confirmation to Azure Service Bus Queue
                await _azureServiceBusClient.SendMessageToQueueAsync(paymentConfirmation);

                return paymentConfirmation;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Payment initiation failed.", ex);
            }
        }
    }
}
