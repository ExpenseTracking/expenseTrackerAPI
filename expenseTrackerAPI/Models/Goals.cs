using System.ComponentModel.DataAnnotations;

namespace expenseTrackerAPI.Models
{
    public class Goals
    {
        public int GoalId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string? Description { get; set; }

        // date of actual goal set
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
