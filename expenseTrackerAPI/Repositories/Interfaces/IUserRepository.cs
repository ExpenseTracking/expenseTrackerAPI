using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        int CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
