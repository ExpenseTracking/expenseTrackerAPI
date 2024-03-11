using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Repositories
{
    public interface IUserRolesRepository
    {
        IEnumerable<UserRoles> GetUserRoles();
        UserRoles GetUserRoleById(int id);
        int CreateUserRole(UserRoles userRole);
        bool UpdateUserRole(UserRoles userRole);
        bool DeleteUserRole(int id);
    }
}
