using FOBOS_API.Data;
using FOBOS_API.Models;
using FOBOS_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using System;

namespace FOBOS_API.Repositories
{
  public class CategoryTypeRepository : _BaseRepository, ICategoryTypeRepository
  {

    public async Task<IList<CategoryType>> GetCategoryTypes()
    {
        try
        {
            db.AbrirConexao();
            string sql = @"SELECT * FROM FOBO_TB_CATEGORY_TYPES";

            IList<CategoryType> categories = (await db.getSQLConnection().QueryAsync<CategoryType>(sql)).ToList();

            db.FecharConexao();
            return categories;
        }
        catch (Exception e)
        {
            db.FecharConexao();
            Console.WriteLine(e);
            throw new NotImplementedException();
        }
    }


        public async Task<IList<CategoryType>> GetCategoryTypesActivated()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_CATEGORY_TYPES"
                             + " WHERE CATY_BL_ATIVO = 1" ;

                IList<CategoryType> categories = (await db.getSQLConnection().QueryAsync<CategoryType>(sql)).ToList();

                db.FecharConexao();
                return categories;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task<CategoryType> GetCategoryType(int id)
    {
        try
        {
            db.AbrirConexao();
            string sql = @"SELECT * FROM FOBO_TB_CATEGORY_TYPES" +
                        " WHERE CATY_SQ_CODIGO = @id";

            CategoryType categoryType = await db.getSQLConnection().QueryFirstOrDefaultAsync<CategoryType>(sql, new { id = id });

            db.FecharConexao();
            return categoryType;

        }
        catch (Exception e)
        {
            db.FecharConexao();
            Console.WriteLine(e);
            throw new NotImplementedException();
        }
    }

        public async Task SaveCategoryType(CategoryType categoryType)
        {
            try
            {
                db.AbrirConexao();

                if (categoryType.id == null || categoryType.id == 0)
                {
                    await Insert(categoryType);
                }
                else
                {
                    await Update(categoryType);
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

        private async Task Insert(CategoryType categoryType)
        {

            try
            {
                string sql = @" INSERT INTO FOBO_TB_CATEGORY_TYPES "
                            + "("
                                + " CATY_NM_NAME,"
                                + " CATY_TP_STATEMENT,"
                                + " CATY_DT_CREATED_AT,"
                                + " CATY_DT_UPDATED_AT,"
                                + " CATY_BL_ATIVO"
                            + " ) VALUES ("
                                + " @name, "
                                + " @typeStatement, "
                                + " @createdAt,"
                                + " @now,"
                                + " 1 "
                            + " )";

                await db.getSQLConnection().ExecuteAsync(sql, categoryType);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }

        private async Task Update(CategoryType categoryType)
        {

            try
            {
                string sql = @" UPDATE FOBO_TB_CATEGORY_TYPES SET"
                                + " CATY_NM_NAME = @name, "
                                + " CATY_TP_STATEMENT = @typeStatement, "
                                + " CATY_DT_CREATED_AT = @createdAt,"
                                + " CATY_DT_UPDATED_AT = @now,"
                                + " CATY_BL_ATIVO = @ativo"
                                + " WHERE CATY_SQ_CODIGO = @id";

                await db.getSQLConnection().ExecuteAsync(sql, categoryType);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }

    }

}