using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Services
{
    public interface IIncomeSourceService
    {
        IEnumerable<IncomeSource> GetIncomeSources();
        IncomeSource GetIncomeSourceById(int id);
        int CreateIncomeSource(IncomeSource incomeSource);
        bool UpdateIncomeSource(IncomeSource incomeSource);
        bool DeleteIncomeSource(int id);
    }
}
