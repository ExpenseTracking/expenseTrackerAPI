using expenseTrackerAPI.Models;
using expenseTrackerAPI.Models.User;

namespace expenseTrackerAPI.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        int GetUserId(Account account);
        int CreateUser(Account account);
        bool UpdateUser(Account account);
        bool DeleteUser(int id);
    }
}
