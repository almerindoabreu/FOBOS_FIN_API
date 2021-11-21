using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task SaveUser(User user);
        Task<User> GetUser(int id);
        Task<IList<User>> GetUsers();
        Task<IList<User>> GetUsersActivated();
    }
}
