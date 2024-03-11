using System.ComponentModel.DataAnnotations;

namespace expenseTrackerAPI.Models;

public class IncomeSource
{
    public int IncomeSourceId { get; set; }

    [Required]
    public int UserId { get; set; }

    public string IncomeSourceName { get; set; }

    public bool? IsDeleted { get; set; }
}
