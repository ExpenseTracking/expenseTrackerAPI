using expenseTrackerAPI.Models;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class IncomeSourceService : IIncomeSourceService
    {
        private readonly IIncomeSourceRepository _incomeSourceRepository;

        public IncomeSourceService(IIncomeSourceRepository incomeSourceRepository)
        {
            _incomeSourceRepository = incomeSourceRepository;
        }

        public IEnumerable<IncomeSource> GetIncomeSources()
        {
            return _incomeSourceRepository.GetIncomeSources();
        }

        public IEnumerable<IncomeSource> GetIncomeSourceByUserId(int id)
        {
            return _incomeSourceRepository.GetIncomeSourceByUserId(id);
        }

        public int CreateIncomeSource(IncomeSource incomeSource)
        {
            return _incomeSourceRepository.CreateIncomeSource(incomeSource);
        }

        public bool UpdateIncomeSource(IncomeSource incomeSource)
        {
            return _incomeSourceRepository.UpdateIncomeSource(incomeSource);
        }

        public bool DeleteIncomeSource(int id)
        {
            return _incomeSourceRepository.DeleteIncomeSource(id);
        }
    }
}
