using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories.Interfaces
{
    public interface IBankRepository
    {
        Task<IList<Bank>> GetBanks();
        Task<IList<Bank>> GetBanksActivated();
        Task<Bank> GetBank(int id);
        Task SaveBank(Bank bank);
    }
}
