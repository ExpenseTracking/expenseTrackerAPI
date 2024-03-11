using System.ComponentModel.DataAnnotations;

namespace expenseTrackerAPI.Models
{
    public class Income
    {
        public int IncomeId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int IncomeSourceId { get; set; }

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
    }
}
