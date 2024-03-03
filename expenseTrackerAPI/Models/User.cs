namespace expenseTrackerAPI.Models
{
    public class User
    {
        public int userId { get; set; }

        public string username { get; set; }
        
        public string password { get; set; }
        
        public string email { get; set; }
        
        public int roleId { get; set; }
        
        public DateTime createdAt { get; set; }
        
        public DateTime updatedAt { get; set; }

        public DateTime deletedAt { get; set; }

        public bool isDeleted { get; set; }
    }
}