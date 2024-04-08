using expenseTrackerAPI.Models.User;

namespace expenseTrackerAPI.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUserById(int id);
        int CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
