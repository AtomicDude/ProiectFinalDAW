using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;


namespace ProiectFinalDAW.Repositories.OrderDetailsRepository
{
    public class OrderDetailsRepository:GenericRepository<OrderDetail>
    {
        public OrderDetailsRepository(Context C) : base(C)
        {

        }
    }
}
