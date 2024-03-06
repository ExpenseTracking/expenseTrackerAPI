using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models.Expense;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/expense
        [HttpGet]
        public IActionResult GetExpenses()
        {
            try
            {
                var expenses = _expenseService.GetExpenses();
                return StatusCode(200, expenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/expense/{id}
        [HttpGet("{id}")]
        public IActionResult GetExpenseById(int id)
        {
            try
            {
                var expense = _expenseService.GetExpenseById(id);
                if (expense == null)
                {
                    return StatusCode(404, $"No expense found with id: {id}");
                }
                return StatusCode(200, expense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/expense
        [HttpPost]
        public IActionResult CreateExpense(Expense expense)
        {
            try
            {
                var id = _expenseService.CreateExpense(expense);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/expense/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateExpense(Expense expense)
        {
            try
            {
                var updated = _expenseService.UpdateExpense(expense);
                if(updated)
                {
                    return StatusCode(200, $"Successfully updated expenseId: {expense.ExpenseId}");
                }
                else
                {
                    return StatusCode(400, $"Unable to update expenseId: {expense.ExpenseId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/expense/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            try
            {
                var deleted = _expenseService.DeleteExpense(id);
                if (deleted)
                {
                    return StatusCode(200, $"Successfully deleted expenseId: {id}");
                }
                else
                {
                    return StatusCode(400, $"Unable to delete expenseId: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
