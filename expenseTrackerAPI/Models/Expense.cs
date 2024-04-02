using System.ComponentModel.DataAnnotations;

namespace expenseTrackerAPI.Models.Expense
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal Amount { get; set; }

        // date of actual expense occurring
        [Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }

        public string? transactionTypeName { get; set; }

        public string? UserName { get; set; }
    }
}
