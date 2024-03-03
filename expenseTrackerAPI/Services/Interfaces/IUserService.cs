using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        int CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
