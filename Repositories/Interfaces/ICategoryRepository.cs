using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IList<Category>> GetCategories();
        Task<IList<Category>> GetCategoriesActivated();
        Task<Category> GetCategory(int id);
        Task<IList<Category>> GetCategoriesByCategoryType(int id);
        Task SaveCategory(Category Categorys);
    }
}
