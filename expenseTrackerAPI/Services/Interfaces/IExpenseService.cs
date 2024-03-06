using expenseTrackerAPI.Models.Expense;

namespace expenseTrackerAPI.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetExpenses();
        Expense GetExpenseById(int id);
        int CreateExpense(Expense expense);
        bool UpdateExpense(Expense expense);
        bool DeleteExpense(int id);
    }
}
