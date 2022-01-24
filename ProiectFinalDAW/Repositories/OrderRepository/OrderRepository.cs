using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;

namespace ProiectFinalDAW.Repositories.OrderRepository
{
    public class OrderRepository:GenericRepository<Order>
    {
        public OrderRepository(Context C) : base(C)
        {

        }
    }
}
