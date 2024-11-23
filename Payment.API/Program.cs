
using Payment.API.Services;

namespace Payment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register Payment Services
            builder.Services.AddScoped<IPaymentProcessor, PaymentProcessor>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            // Register Azure Service Bus Client
            builder.Services.AddScoped<IAzureServiceBusClient, AzureServiceBusClient>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
