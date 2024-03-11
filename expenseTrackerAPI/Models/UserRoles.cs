using System.ComponentModel.DataAnnotations;

namespace expenseTrackerAPI.Models
{
    public class UserRoles
    {
        public int RoleId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }

    }
}