using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Repositories.GenericRepository;

namespace ProiectFinalDAW.Repositories.UserRepository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        User GetByUsername(string name);
        User GetUserAndFavouriteAddress(string name);
        User GetAllOrders(string name);
    }
}
