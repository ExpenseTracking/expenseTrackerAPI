using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models.User;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return StatusCode(404, $"No user found with id: {id}");
                }
                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/user
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            try
            {
                var id = _userService.CreateUser(user);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                var updated = _userService.UpdateUser(user);
                if(updated)
                {
                    return StatusCode(200, $"Successfully updated userId: {user.UserId}");
                }
                else
                {
                    return StatusCode(400, $"Unable to update userId: {user.UserId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var deleted = _userService.DeleteUser(id);
                if (deleted)
                {
                    return StatusCode(200, $"Successfully deleted userId: {id}");
                }
                else
                {
                    return StatusCode(400, $"Unable to delete userId: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
