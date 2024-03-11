using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Repositories
{
    public interface ITransactionTypeRepository
    {
        IEnumerable<TransactionType> GetTransactionTypes();
        IEnumerable<TransactionType> GetTransactionTypeByUserId(int id);
        int CreateTransactionType(TransactionType transactionType);
        bool UpdateTransactionType(TransactionType transactionType);
        bool DeleteTransactionType(int id);
    }
}
