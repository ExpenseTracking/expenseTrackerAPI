using System.Data.SqlTypes;

namespace expenseTrackerAPI.Models.Expense
{
    public class Expense
    {
        public int expenseId { get; set; }

        public int userId { get; set; }

        public int transactionTypeId { get; set; }

        public decimal amount { get; set; }

        // date of actual expense occurring
        public DateTime date { get; set; }

        public string? description { get; set; }

        public DateTime? createdAt { get; set; }

        public DateTime? updatedAt { get; set; }

        public DateTime? deletedAt { get; set; }

        public bool? isDeleted { get; set; }
    }
}
