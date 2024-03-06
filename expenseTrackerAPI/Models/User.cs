using System.ComponentModel.DataAnnotations;

namespace expenseTrackerAPI.Models.User
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public int RoleId { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}