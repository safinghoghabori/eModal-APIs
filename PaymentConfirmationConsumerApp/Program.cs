using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentConfirmationConsumerApp.Services;

namespace PaymentConfirmationConsumerApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Create a Host Builder for dependency injection
            var host = CreateHostBuilder(args).Build();

            // Get the AzureServiceBusClient instance from DI container
            var serviceBusClient = host.Services.GetRequiredService<IAzureServiceBusClient>();

            // Start receiving messages from Service Bus
            await serviceBusClient.ReceiveMessagesAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            // Register services in the DI container
            services.AddScoped<IAzureServiceBusClient, AzureServiceBusClient>();
            services.AddScoped<IPaymentConfirmationHandler, PaymentConfirmationHandler>();
        });

    }
}
