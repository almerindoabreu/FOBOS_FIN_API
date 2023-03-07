using FOBOS_API.Data;
using FOBOS_API.Models;
using FOBOS_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using Dapper;
using Z.Dapper.Plus;

namespace FOBOS_API.Repositories
{
    public class StatementRepository : _BaseRepository, IStatementRepository
    {
        public async Task BulkStatement(IList<Statement> Statements)
        {
            try
            {
                db.AbrirConexao();

                db.getSQLConnection().BulkInsert(Statements);

                db.FecharConexao();
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
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

        public async Task<bool> SaveStatement(Statement Statement)
        {
            try
            {
                db.AbrirConexao();

                if (Statement.id == null || Statement.id == 0)
                {
                    await Insert(Statement);
                }
                else
                {
                    await Update(Statement);
                }

                db.FecharConexao();
                return true;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                return false;
                throw new NotImplementedException();
            }
        }

        private async Task Insert(Statement statement)
        {
            try
            {
                string sql = @" INSERT INTO FOBO_TB_STATEMENTS "
                            + "("
                                + " STAT_NM_NAME,"
                                + " STAT_DS_DESCRIPTION,"
                                + " STAT_NR_VALUE,"
                                + " STAT_DT_DATE,"
                                + " STAT_NR_BALANCE"
                            + " ) VALUES ("
                                + " @name, "
                                + " @description,"
                                + " @value,"
                                + " @date,"
                                + " @balance"
                            + " )";

                await db.getSQLConnection().ExecuteAsync(sql, statement);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }
        }

        private async Task Update(Statement statement)
        {

            try
            {
                string sql = @" UPDATE FOBO_TB_STATEMENTS SET"
                                + " STAT_NM_NAME = @name, "
                                + " STAT_DT_DATE = @date,"
                                + " STAT_NR_VALUE = @value,"
                                + " STAT_NR_BALANCE = @balacen,"
                                + " STAT_DS_DESCRIPTION = @description"
                                + " WHERE BANK_SQ_CODIGO = @id";

                await db.getSQLConnection().ExecuteAsync(sql, statement);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }
        }
    }
    
    }