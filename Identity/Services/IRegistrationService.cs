using Identity.Models;

namespace Identity.Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(User user);
    }
}
