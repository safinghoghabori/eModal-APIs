using Identity.Database;
using Identity.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _databaseContext;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(ApplicationDBContext databaseContext, JwtTokenGenerator jwtTokenGenerator)
        {
            _databaseContext = databaseContext;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
    
        public async Task<object> LoginAsync(string email, string password, string userType)
        {
                var user = await _databaseContext.Users
                    .Where(tc => tc.Email == email && tc.Password == password).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("Invalid credentials.");
                }

                var token = _jwtTokenGenerator.GenerateToken(email, userType, user.Id);

                var result = new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                };

                return new {data = result, token}; 
        }
    }
}
