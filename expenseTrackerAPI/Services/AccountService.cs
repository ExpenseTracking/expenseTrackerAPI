using expenseTrackerAPI.Models;
using expenseTrackerAPI.Models.User;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository userRepository)
        {
            _accountRepository = userRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public int GetUserId(Account account)
        {
            return _accountRepository.GetUserId(account);
        }

        public int CreateUser(Account account)
        {
            return _accountRepository.CreateUser(account);
        }

        public bool UpdateUser(Account account)
        {
            return _accountRepository.UpdateUser(account);
        }

        public bool DeleteUser(int id)
        {
            return _accountRepository.DeleteUser(id);
        }
    }
}
