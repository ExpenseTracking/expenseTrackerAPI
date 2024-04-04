using expenseTrackerAPI.Models;

namespace expenseTrackerAPI.Repositories
{
    public interface IGoalsRepository
    {
        IEnumerable<Goals> GetGoals();
        IEnumerable<Goals> GetGoalsByUserId(int id);
        int CreateGoal(Goals goal);
        bool UpdateGoal(Goals goal);
        bool DeleteGoal(int id);
    }
}
