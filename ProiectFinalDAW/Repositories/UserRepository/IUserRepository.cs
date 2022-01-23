using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Repositories.GenericRepository;

namespace ProiectFinalDAW.Repositories.UserRepository
{
    interface IUserRepository:IGenericRepository<User>
    {
        User GetByUsername(string name);
    }
}
