using Identity.Database;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDBContext _databaseContext;

        public RegistrationService(ApplicationDBContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            // Validate if email is already taken
            if (await IsEmailExists(user.Email))
            {
                throw new Exception("Email already exists.");
            }

            // Add user to the database
            user.Id = Guid.NewGuid().ToString();
            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _databaseContext.Users.AnyAsync(tc => tc.Email == email);
        }
    }
}
