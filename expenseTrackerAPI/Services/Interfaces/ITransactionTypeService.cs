using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Services
{
    public interface ITransactionTypeService
    {
        IEnumerable<TransactionType> GetTransactionTypes();
        IEnumerable<TransactionType> GetTransactionTypeByUserId(int id);
        int CreateTransactionType(TransactionType transactionType);
        bool UpdateTransactionType(TransactionType transactionType);
        bool DeleteTransactionType(int id);
    }
}
