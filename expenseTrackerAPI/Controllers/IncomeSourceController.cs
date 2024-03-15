using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeSourceController : ControllerBase
    {
        private readonly IIncomeSourceService _incomeSourceService;

        public IncomeSourceController(IIncomeSourceService incomeSourceService)
        {
            _incomeSourceService = incomeSourceService;
        }

        // GET: api/incomeSource
        [HttpGet]
        public IActionResult GetIncomeSources()
        {
            try
            {
                var incomeSources = _incomeSourceService.GetIncomeSources();
                return StatusCode(200, incomeSources);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/incomeSource/{id}
        [HttpGet("{id}")]
        public IActionResult GetIncomeSourceByUserId(int id)
        {
            try
            {
                var incomeSource = _incomeSourceService.GetIncomeSourceByUserId(id);
                if (incomeSource == null)
                {
                    return StatusCode(404, $"No income source found for user id: {id}");
                }
                return StatusCode(200, incomeSource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/incomeSource
        [HttpPost]
        public IActionResult CreateIncomeSource(IncomeSource incomeSource)
        {
            try
            {
                var id = _incomeSourceService.CreateIncomeSource(incomeSource);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/incomeSource
        [HttpPut]
        public IActionResult UpdateIncomeSource(IncomeSource incomeSource)
        {
            try
            {
                var updated = _incomeSourceService.UpdateIncomeSource(incomeSource);
                if(updated)
                {
                    return StatusCode(200, $"Successfully updated income source id: {incomeSource.IncomeSourceId}");
                }
                else
                {
                    return StatusCode(400, $"Unable to update income source id: {incomeSource.IncomeSourceId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/incomeSource/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteIncomeSource(int id)
        {
            try
            {
                var deleted = _incomeSourceService.DeleteIncomeSource(id);
                if (deleted)
                {
                    return StatusCode(200, $"Successfully deleted income source id: {id}");
                }
                else
                {
                    return StatusCode(400, $"Unable to delete income source id: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
