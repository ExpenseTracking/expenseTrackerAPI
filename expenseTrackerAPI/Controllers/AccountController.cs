using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/account
        [HttpGet]
        public IActionResult GetAccounts()
        {
            try
            {
                var users = _accountService.GetAccounts();
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/account/{username}/{password}
        [HttpGet("{username}/{password}")]
        public IActionResult GetUserId(string username, string password)
        {
            try
            {
                var account = new Account { Username = username, Password = password };
                var id = _accountService.GetUserId(account);
                if (id == 0)
                {
                    return StatusCode(404, $"Incorrect username or password for: {username}");
                }
                return StatusCode(200, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/account
        [HttpPost]
        public IActionResult CreateUser(Account account)
        {
            try
            {
                var id = _accountService.CreateUser(account);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/account/{id}
        [HttpPut]
        public IActionResult UpdateUser(Account account)
        {
            try
            {
                var updated = _accountService.UpdateUser(account);
                if(updated)
                {
                    return StatusCode(200, account.UserId);
                }
                else
                {
                    return StatusCode(400, $"Unable to update account with userId: {account.UserId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/account/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var deleted = _accountService.DeleteUser(id);
                if (deleted)
                {
                    return StatusCode(200, id);
                }
                else
                {
                    return StatusCode(400, $"Unable to delete account with userId: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
