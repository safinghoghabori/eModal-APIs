using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentConfirmationConsumerApp.Databases;
using PaymentConfirmationConsumerApp.Services;

namespace PaymentConfirmationConsumerApp
{
    public class Program
    {
        private const string _deafultSqlConnectionString = "Server=tcp:emodal.database.windows.net,1433;Initial Catalog=eModalDB;Persist Security Info=False;User ID=safinraytex;Password=S@finraytex10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
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
            services.AddScoped<ISqlDbService, SqlDbService>();
            
            // Register the DbContext using the connection string 
            services.AddDbContext<ApplicationDBContext>(
                options => options.UseSqlServer(_deafultSqlConnectionString));

            // Register CosmosDB service
            services.AddSingleton<ICosmosDbService, CosmosDbService>();
        });
    }
}
