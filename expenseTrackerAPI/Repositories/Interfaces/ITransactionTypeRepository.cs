using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Repositories
{
    public interface ITransactionTypeRepository
    {
        IEnumerable<TransactionType> GetTransactionTypes(TransactionType transactionType);
        TransactionType GetTransactionTypeById(int id);
        int CreateTransactionType(TransactionType transactionType);
        bool UpdateTransactionType(TransactionType transactionType);
        bool DeleteTransactionType(int id);
    }
}
