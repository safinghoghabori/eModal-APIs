
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add Ocelot
            builder.Services.AddOcelot();

            // Add Ocelot.json as a configuration file
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            var app = builder.Build();

            // Enable Ocelot Middleware
            await app.UseOcelot();
            app.MapControllers();

            app.Run();
        }
    }
}
