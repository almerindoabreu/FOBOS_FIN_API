using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories.Interfaces
{
    public interface IStatementRepository
    {
        Task<bool> SaveStatement(Statement Statement);
        Task BulkStatement(IList<Statement> Statements);
        Task<IList<Statement>> GetStatements();
        Task<IList<Statement>> GetStatementsActivated();
        Task<IList<Statement>> GetStatements(string Category, string monthly);
        Task<IList<Statement>> GetStatements(string Category, DateTime start, DateTime end);
        Task<dynamic> GetSumValuesCategories(DateTime start, DateTime end);
        Task<IList<Statement>> GetStatementsWithoutCategory();
        Task<Statement> GetStatement(int id);

        Task<dynamic> GetLastImport();
        Task<dynamic> GetSumMonthlyValuesCategories(DateTime start, DateTime end);
        // Task<IActionResult> GetHigherExpenses();
        // Task<IActionResult> GetLowerExpenses();
    }
}
