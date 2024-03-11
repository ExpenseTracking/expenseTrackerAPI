using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }

        // GET: api/userRoles
        [HttpGet]
        public IActionResult GetUserRoles()
        {
            try
            {
                var userRoles = _userRolesService.GetUserRoles();
                return StatusCode(200, userRoles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/userRole/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserRoleById(int id)
        {
            try
            {
                var userRole = _userRolesService.GetUserRoleById(id);
                if (userRole == null)
                {
                    return StatusCode(404, $"No role found with id: {id}");
                }
                return StatusCode(200, userRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/userRole
        [HttpPost]
        public IActionResult CreateUserRole(UserRoles userRole)
        {
            try
            {
                var id = _userRolesService.CreateUserRole(userRole);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/userRole/{id}
        [HttpPut]
        public IActionResult UpdateUserRole(UserRoles userRole)
        {
            try
            {
                var updated = _userRolesService.UpdateUserRole(userRole);
                if(updated)
                {
                    return StatusCode(200, $"Successfully updated roleId: {userRole.RoleId}");
                }
                else
                {
                    return StatusCode(400, $"Unable to update roleId: {userRole.RoleId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/userRole/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUserRole(int id)
        {
            try
            {
                var deleted = _userRolesService.DeleteUserRole(id);
                if (deleted)
                {
                    return StatusCode(200, $"Successfully deleted roleId: {id}");
                }
                else
                {
                    return StatusCode(400, $"Unable to delete roleId: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
