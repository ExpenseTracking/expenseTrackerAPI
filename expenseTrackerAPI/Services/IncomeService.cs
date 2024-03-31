﻿using expenseTrackerAPI.Models;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public IEnumerable<Income> GetIncome()
        {
            return _incomeRepository.GetIncome();
        }

        public IEnumerable<Income> GetIncomeByUserId(int id)
        {
            return _incomeRepository.GetIncomeByUserId(id);
        }

        public int CreateIncome(Income expense)
        {
            return _incomeRepository.CreateIncome(expense);
        }

        public bool UpdateIncome(Income expense)
        {
            return _incomeRepository.UpdateIncome(expense);
        }

        public bool DeleteIncome(int id)
        {
            return _incomeRepository.DeleteIncome(id);
        }
    }
}
