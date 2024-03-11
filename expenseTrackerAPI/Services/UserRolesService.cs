using expenseTrackerAPI.Models;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public UserRolesService(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        public IEnumerable<UserRoles> GetUserRoles()
        {
            return _userRolesRepository.GetUserRoles();
        }

        public UserRoles GetUserRoleById(int id)
        {
            return _userRolesRepository.GetUserRoleById(id);
        }

        public int CreateUserRole(UserRoles userRoles)
        {
            return _userRolesRepository.CreateUserRole(userRoles);
        }

        public bool UpdateUserRole(UserRoles userRoles)
        {
            return _userRolesRepository.UpdateUserRole(userRoles);
        }

        public bool DeleteUserRole(int id)
        {
            return _userRolesRepository.DeleteUserRole(id);
        }
    }
}
