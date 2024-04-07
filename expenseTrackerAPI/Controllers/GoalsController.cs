using Microsoft.AspNetCore.Mvc;
using expenseTrackerAPI.Models;
using expenseTrackerAPI.Services;

namespace expenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalsService _goalsService;

        public GoalsController(IGoalsService goalsService)
        {
            _goalsService = goalsService;
        }

        // GET: api/goals
        [HttpGet]
        public IActionResult GetGoals()
        {
            try
            {
                var goals = _goalsService.GetGoals();
                return StatusCode(200, goals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/goals/{id}
        [HttpGet("{id}")]
        public IActionResult GetGoalsByUserId(int id)
        {
            try
            {
                var goals = _goalsService.GetGoalsByUserId(id);
                if (goals == null)
                {
                    return StatusCode(404, $"No goals found for user with id: {id}");
                }
                return StatusCode(200, goals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/goals
        [HttpPost]
        public IActionResult CreateGoal(Goals goals)
        {
            try
            {
                var id = _goalsService.CreateGoal(goals);
                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/goals/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateGoal(Goals goals)
        {
            try
            {
                var updated = _goalsService.UpdateGoal(goals);
                if(updated)
                {
                    return StatusCode(200, goals.GoalId);
                }
                else
                {
                    return StatusCode(400, $"Unable to update goalId: {goals.GoalId}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/goals/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteGoal(int id)
        {
            try
            {
                var deleted = _goalsService.DeleteGoal(id);
                if (deleted)
                {
                    return StatusCode(200, id);
                }
                else
                {
                    return StatusCode(400, $"Unable to delete goalId: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
