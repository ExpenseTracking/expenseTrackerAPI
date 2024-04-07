using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        // GET: api/transactionType
        [HttpGet]
        public IActionResult GetTransactionTypes()
        {
            try
            {
                var expenses = _transactionTypeService.GetTransactionTypes();
                return StatusCode(200, expenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/transactionType/{id}
        [HttpGet("{id}")]
        public IActionResult GetTransactionTypeByUserId(int id)
        {
            try
            {
                var transactionType = _transactionTypeService.GetTransactionTypeByUserId(id);
                if (transactionType == null)
                {
                    return StatusCode(404, $"No transaction type found for user id: {id}");
                }
                return StatusCode(200, transactionType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/transactionType
        [HttpPost]
        public IActionResult CreateTransactionType(TransactionType transactionType)
        {
            try
            {
                var id = _transactionTypeService.CreateTransactionType(transactionType);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/transactionType/{id}
        [HttpPut]
        public IActionResult UpdateTransactionType(TransactionType transactionType)
        {
            try
            {
                var updated = _transactionTypeService.UpdateTransactionType(transactionType);
                if(updated)
                {
                    return StatusCode(200, transactionType.TransactionTypeId);
                }
                else
                {
                    return StatusCode(400, $"Unable to update transactionTypeId: {transactionType.TransactionTypeId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/transactionType/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTransactionType(int id)
        {
            try
            {
                var deleted = _transactionTypeService.DeleteTransactionType(id);
                if (deleted)
                {
                    return StatusCode(200, id);
                }
                else
                {
                    return StatusCode(400, $"Unable to delete transactionTypeId: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
