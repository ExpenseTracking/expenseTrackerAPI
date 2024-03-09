using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Services
{
    public interface ITransactionTypeService
    {
        IEnumerable<TransactionType> GetTransactionTypes(TransactionType transactionType);
        TransactionType GetTransactionTypeById(int id);
        int CreateTransactionType(TransactionType transactionType);
        bool UpdateTransactionType(TransactionType transactionType);
        bool DeleteTransactionType(int id);
    }
}
