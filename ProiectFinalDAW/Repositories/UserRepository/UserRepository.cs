using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;
using ProiectFinalDAW.Repositories.GenericRepository;

namespace ProiectFinalDAW.Repositories.UserRepository
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {
        public UserRepository(Context C) : base(C)
        {

        }

        public User GetByUsername(string name)
        {
            return _table.FirstOrDefault(x => x.Username.Equals(name));
        }
    }
}
