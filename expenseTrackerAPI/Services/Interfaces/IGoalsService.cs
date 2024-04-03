using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Services
{
    public interface IGoalsService
    {
        IEnumerable<Goals> GetGoals();
        IEnumerable<Goals> GetGoalsByUserId(int id);
        int CreateGoal(Goals goals);
        bool UpdateGoal(Goals goals);
        bool DeleteGoal(int id);
    }
}
