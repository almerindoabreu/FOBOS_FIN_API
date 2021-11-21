using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories.Interfaces
{
    public interface ICategoryTypeRepository
    {
        Task SaveCategoryType(CategoryType categoryType);
        Task<CategoryType> GetCategoryType(int id);
        Task<IList<CategoryType>> GetCategoryTypes();
        Task<IList<CategoryType>> GetCategoryTypesActivated();
    }
}
