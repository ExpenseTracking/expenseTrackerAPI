using expenseTrackerAPI.Models.Expense;

namespace expenseTrackerAPI.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetExpenses();
        IEnumerable<Expense> GetExpenseByUserId(int id);
        int CreateExpense(Expense expense);
        bool UpdateExpense(Expense expense);
        bool DeleteExpense(int id);
    }
}
