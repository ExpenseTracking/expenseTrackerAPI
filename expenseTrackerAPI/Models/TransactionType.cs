using System.ComponentModel.DataAnnotations;

namespace expenseTrackerAPI.Models
{
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string TransactionTypeName { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
