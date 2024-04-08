using expenseTrackerAPI.Models.User;

namespace expenseTrackerAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUserById(int id);
        int CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
