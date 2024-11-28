using Azure.Messaging.ServiceBus;
using Payment.API.Models;
using System.Text.Json;

namespace Payment.API.Services
{
    public class AzureServiceBusClient: IAzureServiceBusClient
    {
        private readonly string _serviceBusConnectionString = "Endpoint=sb://safinsb007.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=J6lFdZI8fKi4GqCU/UWhT6al94WwHq9fG+ASbBwaazU=";
        private readonly string _queueName = "paymentConfirmation";

        public async Task SendMessageToQueueAsync(PaymentConfirmation paymentConfirmation)
        {
            await using var client = new ServiceBusClient(_serviceBusConnectionString);
            var sender = client.CreateSender(_queueName);
            var message = new ServiceBusMessage(JsonSerializer.Serialize(paymentConfirmation));
            await sender.SendMessageAsync(message);
        }
    }
}
