using expenseTrackerAPI.Models.Expense;

namespace expenseTrackerAPI.Repositories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetExpenses();
        IEnumerable<Expense> GetExpenseByUserId(int id);
        int CreateExpense(Expense expense);
        bool UpdateExpense(Expense expense);
        bool DeleteExpense(int id);
    }
}
