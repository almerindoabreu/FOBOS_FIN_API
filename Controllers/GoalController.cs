using Dapper;
using FOBOS_API.Data;
using FOBOS_API.Models;
using FOBOS_API.Repositories;
using FOBOS_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GoalController : ControllerBase
    {
        private readonly IGoalRepository goalRepository;

        public GoalController(IGoalRepository goalRepository)
        {
            this.goalRepository = goalRepository;
        }

        [HttpGet]
        [Route("goals")]
        public async Task<IList<Goal>> GetGoalsActivated()
        {
            IList<Goal> goals = await goalRepository.GetGoalsActivated();
            return goals;
        }

        [HttpGet]
        [Route("goals/ShowAll")]
        public async Task<IList<Goal>> GetGoals()
        {
            IList<Goal> goals = await goalRepository.GetGoals();
            return goals;
        }

        [HttpGet]
        [Route("goals/{id}")]
        public async Task<Goal> GetGoal([FromRoute] int id)
        {
            Goal goal = await goalRepository.GetGoal(id);
            return goal;
        }

        [HttpPost]
        [Route("save")]
        public async Task SaveGoal([FromBody] Goal goal)
        {
            await goalRepository.SaveGoal(goal);
        }

        [HttpPut]
        [Route("delete/{id}")]
        public async Task DeleteGoal([FromRoute] int id)
        {
            Goal goal = await goalRepository.GetGoal(id);
            goal.ativo = false;

            await goalRepository.SaveGoal(goal);
        }
    }
}
