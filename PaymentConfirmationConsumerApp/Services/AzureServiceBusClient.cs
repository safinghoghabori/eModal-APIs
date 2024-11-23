using Azure.Messaging.ServiceBus;
using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Services
{
    public class AzureServiceBusClient: IAzureServiceBusClient
    {
        private readonly string _serviceBusConnectionString = "Endpoint=sb://safinsb007.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=J6lFdZI8fKi4GqCU/UWhT6al94WwHq9fG+ASbBwaazU=";
        private readonly string _queueName = "paymentConfirmation";

        private readonly IPaymentConfirmationHandler _paymentConfirmationHandler;

        public AzureServiceBusClient(IPaymentConfirmationHandler paymentConfirmationHandler)
        {
            _paymentConfirmationHandler = paymentConfirmationHandler;
        }

        public async Task ReceiveMessagesAsync()
        {
            await using var client = new ServiceBusClient(_serviceBusConnectionString);
            var processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();

            Console.WriteLine("Press any key to stop the worker.");
            Console.ReadKey();

            await processor.StopProcessingAsync();
            await processor.DisposeAsync();
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var paymentInfo = System.Text.Json.JsonSerializer.Deserialize<PaymentConfirmation>(args.Message.Body.ToString());
            Console.WriteLine($"payment uid: {paymentInfo.UserId}, successful: {paymentInfo.IsSuccessful}");

            // Simulate payment processing
            await Task.Delay(1000);

            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
