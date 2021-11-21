using Dapper;
using Dapper.Contrib.Extensions;
using FOBOS_API.Data;
using FOBOS_API.Models;
using FOBOS_API.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories
{
    public class BankRepository : _BaseRepository, IBankRepository
    {
        public BankRepository() : base()
        {
        }

        public async Task<IList<Bank>> GetBanks()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_BANKS";
                IList<Bank> banks = (await db.getSQLConnection().QueryAsync<Bank>(sql)).ToList();

                db.FecharConexao();
                return banks;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<IList<Bank>> GetBanksActivated()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_BANKS"
                            + " WHERE BANK_BL_ATIVO = 1 ";
                IList<Bank> banks = (await db.getSQLConnection().QueryAsync<Bank>(sql)).ToList();

                db.FecharConexao();
                return banks;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<Bank> GetBank(int id)
        {
            try{ 
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_BANKS" +
                            " WHERE BANK_SQ_CODIGO = @id";

                Bank bank = await db.getSQLConnection().QueryFirstOrDefaultAsync<Bank>(sql, new { id = id });

                db.FecharConexao();
                return bank;

            }catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task SaveBank(Bank bank)
        {
            try
            {
                db.AbrirConexao();

                if(bank.id == null || bank.id == 0){
                    await Insert(bank);
                }else
                {
                    await Update(bank);
                }

                db.FecharConexao();
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        private async Task Insert(Bank bank)
        {

            try { 
            string sql = @" INSERT INTO FOBO_TB_BANKS "
                        + "("
                            + " BANK_NM_NAME,"
                            + " BANK_DT_CREATED_AT,"
                            + " BANK_DT_UPDATED_AT,"
                            + " BANK_BL_ATIVO"
                        + " ) VALUES ("
                            + " @name, "
                            + " @createdAt,"
                            + " @now,"
                            + " 1 "
                        + " )";

            await db.getSQLConnection().ExecuteAsync(sql, bank);

            }catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }

        private async Task Update(Bank bank)
        {

            try
            {
                string sql = @" UPDATE FOBO_TB_BANKS SET"
                                + " BANK_NM_NAME = @name, "
                                + " BANK_DT_CREATED_AT = @createdAt,"
                                + " BANK_DT_UPDATED_AT = @now,"
                                + " BANK_BL_ATIVO = @ativo"
                                + " WHERE BANK_SQ_CODIGO = @id";

                await db.getSQLConnection().ExecuteAsync(sql, bank);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }


    }
}
