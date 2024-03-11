using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Repositories
{
    public interface IIncomeRepository
    {
        IEnumerable<Income> GetIncome();
        Income GetIncomeById(int id);
        int CreateIncome(Income income);
        bool UpdateIncome(Income income);
        bool DeleteIncome(int id);
    }
}
