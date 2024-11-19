using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
