using expenseTrackerAPI.Models;
using expenseTrackerAPI.Repositories;

namespace expenseTrackerAPI.Services
{
    public class GoalsService : IGoalsService
    {
        private readonly IGoalsRepository _goalsRepository;

        public GoalsService(IGoalsRepository goalsRepository)
        {
            _goalsRepository = goalsRepository;
        }

        public IEnumerable<Goals> GetGoals()
        {
            return _goalsRepository.GetGoals();
        }

        public IEnumerable<Goals> GetGoalsByUserId(int id)
        {
            return _goalsRepository.GetGoalsByUserId(id);
        }

        public int CreateGoal(Goals goal)
        {
            return _goalsRepository.CreateGoal(goal);
        }

        public bool UpdateGoal(Goals goal)
        {
            return _goalsRepository.UpdateGoal(goal);
        }

        public bool DeleteGoal(int id)
        {
            return _goalsRepository.DeleteGoal(id);
        }
    }
}
