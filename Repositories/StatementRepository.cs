using FOBOS_API.Data;
using FOBOS_API.Models;
using FOBOS_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using Dapper;

namespace FOBOS_API.Repositories
{
  public class StatementRepository : _BaseRepository, IStatementRepository
  {
        public void BulkStatement(List<Statement> Statements)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStatement(int id)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetLastImport()
        {
            throw new NotImplementedException();
        }

        public async Task<Statement> GetStatement(int id)
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_CARDS"
                            + " WHERE CARD_SQ_CODIGO = @id";

                Statement statement = await db.getSQLConnection().QueryFirstOrDefaultAsync<Statement>(sql, new { id = id });

                db.FecharConexao();
                return statement;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<IList<Statement>> GetStatements()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_STATEMENTS";
                IList<Statement> statements = (await db.getSQLConnection().QueryAsync<Statement>(sql)).ToList();

                db.FecharConexao();
                return statements;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<IList<Statement>> GetStatementsActivated()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_STATEMENTS " +
                              " STAT_BL_ATIVO = 1";
                IList<Statement> statements = (await db.getSQLConnection().QueryAsync<Statement>(sql)).ToList();

                db.FecharConexao();
                return statements;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<IList<Statement>> GetStatements(string category, DateTime start, DateTime end)
    {
            throw new System.NotImplementedException();
        }

    public async Task<IList<Statement>> GetStatements(string category, string month)
    {
            throw new System.NotImplementedException(); ;
    }

 

        public async Task<IList<Statement>> GetStatementsWithoutCategory()
    {
            throw new System.NotImplementedException();
        }

        public Task<dynamic> GetSumMonthlyValuesCategories(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetSumValuesCategories(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public async Task SaveStatement(Statement Statement)
    {
            throw new System.NotImplementedException();
        }
    }
}