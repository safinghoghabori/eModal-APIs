using Microsoft.EntityFrameworkCore;
using PaymentConfirmationConsumerApp.Models;

namespace PaymentConfirmationConsumerApp.Databases
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<PaymentConfirmation> PaymentConfirmations { get; set; }
    }
}
