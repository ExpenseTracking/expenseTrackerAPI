using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        // GET: api/income
        [HttpGet]
        public IActionResult GetIncome()
        {
            try
            {
                var income = _incomeService.GetIncome();
                return StatusCode(200, income);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/income/{id}
        [HttpGet("{id}")]
        public IActionResult GetIncomeByUserId(int id)
        {
            try
            {
                var income = _incomeService.GetIncomeByUserId(id);
                if (income == null)
                {
                    return StatusCode(404, $"No income found with id: {id}");
                }
                return StatusCode(200, income);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/income
        [HttpPost]
        public IActionResult CreateIncome(Income income)
        {
            try
            {
                var id = _incomeService.CreateIncome(income);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/income/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateIncome(Income income)
        {
            try
            {
                var updated = _incomeService.UpdateIncome(income);
                if(updated)
                {
                    return StatusCode(200, $"Successfully updated incomeId: {income.IncomeId}");
                }
                else
                {
                    return StatusCode(400, $"Unable to update incomeId: {income.IncomeId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/income/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteIncome(int id)
        {
            try
            {
                var deleted = _incomeService.DeleteIncome(id);
                if (deleted)
                {
                    return StatusCode(200, $"Successfully deleted incomeId: {id}");
                }
                else
                {
                    return StatusCode(400, $"Unable to delete incomeId: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
