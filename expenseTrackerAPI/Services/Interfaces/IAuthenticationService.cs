using expenseTrackerAPI.Models.User;

namespace expenseTrackerAPI.Services
{
    public interface IAuthenticationService
    {
        User? AuthenticateUser(User user);
    }
}