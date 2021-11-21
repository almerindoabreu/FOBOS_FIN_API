using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories.Interfaces
{
    public interface IGoalRepository
    {
        Task SaveGoal(Goal goal);
        void BulkGoal(List<Goal> goals);
        Task<Goal> GetGoal(int id);
        Task<IList<Goal>> GetGoals();
        Task<IList<Goal>> GetGoalsActivated();
        Task<dynamic> GetGoals(string userName, int year);
    }
}
