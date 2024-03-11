using expenseTrackerAPI.Models;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public TransactionTypeService(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        }

        public IEnumerable<TransactionType> GetTransactionTypes()
        {
            return _transactionTypeRepository.GetTransactionTypes();
        }

        public IEnumerable<TransactionType> GetTransactionTypeByUserId(int id)
        {
            return _transactionTypeRepository.GetTransactionTypeByUserId(id);
        }

        public int CreateTransactionType(TransactionType transactionType)
        {
            return _transactionTypeRepository.CreateTransactionType(transactionType);
        }

        public bool UpdateTransactionType(TransactionType transactionType)
        {
            return _transactionTypeRepository.UpdateTransactionType(transactionType);
        }

        public bool DeleteTransactionType(int id)
        {
            return _transactionTypeRepository.DeleteTransactionType(id);
        }
    }
}
