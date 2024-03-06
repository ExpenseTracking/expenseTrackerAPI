using expenseTrackerAPI.Models.Expense;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _expenseRepository.GetExpenses();
        }

        public Expense GetExpenseById(int id)
        {
            return _expenseRepository.GetExpenseById(id);
        }

        public int CreateExpense(Expense expense)
        {
            return _expenseRepository.CreateExpense(expense);
        }

        public bool UpdateExpense(Expense expense)
        {
            return _expenseRepository.UpdateExpense(expense);
        }

        public bool DeleteExpense(int id)
        {
            return _expenseRepository.DeleteExpense(id);
        }
    }
}
