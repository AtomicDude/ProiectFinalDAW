using System;
using ProiectFinalDAW.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Data;
using ProiectFinalDAW.Repositories.GenericRepository;

namespace ProiectFinalDAW.Repositories.FavouriteAddressRepository
{
    public class FavouriteAddressRepository:GenericRepository<FavouriteAddress>, IFavouriteAddressRepository
    {
        public FavouriteAddressRepository(Context C) : base(C)
        {

        }
    }
}
