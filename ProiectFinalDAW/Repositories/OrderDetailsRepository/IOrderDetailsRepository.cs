using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;

namespace ProiectFinalDAW.Repositories.OrderDetailsRepository
{
    public interface IOrderDetailsRepository:IGenericRepository<OrderDetail>
    {
    }
}
