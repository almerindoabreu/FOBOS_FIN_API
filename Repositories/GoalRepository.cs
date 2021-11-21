using FOBOS_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Dapper;
using FOBOS_API.Models;

namespace FOBOS_API.Repositories
{
  public class GoalRepository : _BaseRepository, IGoalRepository
    {

    public async Task<IList<Goal>> GetGoals()
    {
        try
        {
            db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_GOALS";

            IList<Goal> goals = (await db.getSQLConnection().QueryAsync<Goal>(sql)).ToList();

            db.FecharConexao();
            return goals;

        }
        catch (Exception e)
        {
            db.FecharConexao();
            Console.WriteLine(e);
            throw new NotImplementedException();
        }
    }

        public async Task<IList<Goal>> GetGoalsActivated()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_GOALS"
                              + " WHERE GOAL_BL_ATIVO = 1";

                IList<Goal> goals = (await db.getSQLConnection().QueryAsync<Goal>(sql)).ToList();

                db.FecharConexao();
                return goals;

            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }


        public async Task<Goal> GetGoal(int id)
    {
        try
        {
            db.AbrirConexao();
            string sql = @"SELECT * FROM FOBO_TB_GOALS" +
                        " WHERE GOAL_SQ_CODIGO = @id";

            Goal goal = await db.getSQLConnection().QueryFirstOrDefaultAsync<Goal>(sql, new { id = id });

            db.FecharConexao();
            return goal;

        }
        catch (Exception e)
        {
            db.FecharConexao();
            Console.WriteLine(e);
            throw new NotImplementedException();
        }
        }

        public Task SaveGoal(Goal goal)
        {
            throw new NotImplementedException();
        }

        public void BulkGoal(List<Goal> goals)
        {
            throw new NotImplementedException();
        }



        public Task<dynamic> GetGoals(string userName, int year)
        {
            throw new NotImplementedException();
        }
    }

}