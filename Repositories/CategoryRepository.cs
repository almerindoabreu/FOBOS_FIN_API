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
    public class CategoryRepository : _BaseRepository, ICategoryRepository
    {
        public async Task<IList<Category>> GetCategories()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_CATEGORIES";
                IList<Category> categories = (await db.getSQLConnection().QueryAsync<Category>(sql)).ToList();

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

        public async Task<IList<Category>> GetCategoriesActivated()
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_CATEGORIES c"
                            + " JOIN FOBO_TB_CATEGORY_TYPES ct"
                            + " ON c.CATE_FK_CATY_CODIGO = ct.CATY_SQ_CODIGO"
                            + " WHERE c.CATE_BL_ATIVO = 1";
                IList<Category> categories = (await db.getSQLConnection().QueryAsync<Category, CategoryType, Category>(sql,
                    (c, ct) => 
                    {
                        c.CategoryType = ct;
                        return c;
                    },
                splitOn: "CATE_SQ_CODIGO,CATY_SQ_CODIGO")).ToList();

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

        public async Task<Category> GetCategory(int id)
        {
            try
            {
                db.AbrirConexao();
                string sql = @"SELECT * FROM FOBO_TB_CATEGORIES c"
                            + " JOIN FOBO_TB_CATEGORY_TYPES ct"
                            + " ON c.CATE_FK_CATY_CODIGO = ct.CATY_SQ_CODIGO"
                            + " WHERE c.CATE_SQ_CODIGO = @id";

                Category category = (await db.getSQLConnection().QueryAsync<Category, CategoryType, Category>(sql, 
                    (c, ct) =>
                    {
                        c.CategoryType = ct;
                        return c;
                    },
                    splitOn: "CATE_SQ_CODIGO,CATY_SQ_CODIGO",
                    param: new { id = id })).First();

                db.FecharConexao();
                return category;
            }
            catch (Exception e)
            {
                db.FecharConexao();
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }

        public async Task SaveCategory(Category Category)
        {
            try
            {
                db.AbrirConexao();

                if (Category.id == null || Category.id == 0)
                {
                    await Insert(Category);
                }
                else
                {
                    await Update(Category);
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

        private async Task Insert(Category category)
        {

            try
            {
                string sql = @" INSERT INTO FOBO_TB_CATEGORIES "
                            + "("
                                + " CATE_NM_NAME,"
                                + " CATE_FK_CATY_CODIGO,"
                                + " CATE_DT_CREATED_AT,"
                                + " CATE_DT_UPDATED_AT,"
                                + " CATE_BL_ATIVO"
                            + " ) VALUES ("
                                + " @name, "
                                + " @fkCategoryType, "
                                + " @createdAt,"
                                + " @now,"
                                + " 1 "
                            + " )";

                await db.getSQLConnection().ExecuteAsync(sql, category);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }

        private async Task Update(Category category)
        {

            try
            {
                string sql = @" UPDATE FOBO_TB_CATEGORIES SET"
                                + " CATE_NM_NAME = @name, "
                                + " CATE_FK_CATY_CODIGO = @fkCategoryType, "
                                + " CATE_DT_CREATED_AT = @createdAt,"
                                + " CATE_DT_UPDATED_AT = @now,"
                                + " CATE_BL_ATIVO = @ativo"
                                + " WHERE CATE_SQ_CODIGO = @id";

                await db.getSQLConnection().ExecuteAsync(sql, category);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                throw new Exception("ERRO: " + ex.Message);
            }

        }
    }
}