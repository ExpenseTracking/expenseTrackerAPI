using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Services
{
    public interface IIncomeService
    {
        IEnumerable<Income> GetIncome();
        IEnumerable<Income> GetIncomeByUserId(int id);
        int CreateIncome(Income income);
        bool UpdateIncome(Income income);
        bool DeleteIncome(int id);
    }
}
