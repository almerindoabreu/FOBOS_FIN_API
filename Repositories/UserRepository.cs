using FOBOS_API.Data;
using FOBOS_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using System;
using FOBOS_API.Models;

namespace FOBOS_API.Repositories
{
  public class UserRepository : _BaseRepository, IUserRepository
  {

        public async Task<IList<User>> GetUsers()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_USERS";

                IList<User> users = (await db.getSQLConnection().QueryAsync<User>(sql)).ToList();

                db.FecharConexao();
                return users;

            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }
        public async Task<User> GetUser(int id)
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_USERS" +
                            " WHERE USER_SQ_CODIGO = @id";

                User user = await db.getSQLConnection().QueryFirstOrDefaultAsync<User>(sql, new { id = id });

                db.FecharConexao();
                return user;

            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<IList<User>> GetUsersActivated()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_USERS"
                             + " WHERE USER_BL_ATIVO = 1";

                IList<User> users = (await db.getSQLConnection().QueryAsync<User>(sql)).ToList();

                db.FecharConexao();
                return users;

            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task SaveUser(User user)
        {
            try
            {
                db.AbrirConexao();

                if (user.id == null || user.id == 0)
                {
                    await Insert(user);
                }
                else
                {
                    await Update(user);
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

        private async Task Insert(User user)
        {
            try
            {
                string sql = @" INSERT INTO FOBO_TB_USERS "
                            + "("
                                + " USER_NM_NAME"
                            + " ) VALUES ("
                                + " @name "
                            + " )";

                await db.getSQLConnection().ExecuteAsync(sql, user);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }

        private async Task Update(User user)
        {

            try
            {
                string sql = @" UPDATE FOBO_TB_USERS SET"
                                + " USER_NM_NAME = @name "
                                + " WHERE USER_SQ_CODIGO = @id";

                await db.getSQLConnection().ExecuteAsync(sql, user);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }

    }
}