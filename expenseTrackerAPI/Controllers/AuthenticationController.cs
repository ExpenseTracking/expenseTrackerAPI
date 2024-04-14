using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models.User;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult AuthenticateUser(User user)
        {
            try
            {
                var res = _authenticationService.AuthenticateUser(user);
                if (res == null)
                {
                    return StatusCode(404, $"No account found associated with {user.Username}");
                }
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
