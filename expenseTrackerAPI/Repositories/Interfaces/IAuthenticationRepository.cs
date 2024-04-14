using expenseTrackerAPI.Models.User;

namespace expenseTrackerAPI.Repositories
{
    public interface IAuthenticationRepository
    {
        bool AuthenticateUser(User user);

        User GetAuthenticatedUser(User user);
    }
}
