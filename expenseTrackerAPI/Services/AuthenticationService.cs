using System.Linq.Expressions;
using expenseTrackerAPI.Models.User;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public User? AuthenticateUser(User user)
        {
            try
            {
                bool match = _authenticationRepository.AuthenticateUser(user);

                if (match)
                {
                    user = _authenticationRepository.GetAuthenticatedUser(user);

                    return user;
                }
                else
                {
                    throw new Exception("No user found");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
