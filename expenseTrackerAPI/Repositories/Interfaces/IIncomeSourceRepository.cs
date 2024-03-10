using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Repositories
{
    public interface IIncomeSourceRepository
    {
        IEnumerable<IncomeSource> GetIncomeSources();
        IncomeSource GetIncomeSourceById(int id);
        int CreateIncomeSource(IncomeSource incomeSource);
        bool UpdateIncomeSource(IncomeSource incomeSource);
        bool DeleteIncomeSource(int id);
    }
}
