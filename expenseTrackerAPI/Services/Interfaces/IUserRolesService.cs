using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Services
{
    public interface IUserRolesService
    {
        IEnumerable<UserRoles> GetUserRoles();
        UserRoles GetUserRoleById(int id);
        int CreateUserRole(UserRoles userRole);
        bool UpdateUserRole(UserRoles userRole);
        bool DeleteUserRole(int id);
    }
}
