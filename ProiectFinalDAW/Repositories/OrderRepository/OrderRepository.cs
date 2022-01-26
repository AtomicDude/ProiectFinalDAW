using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;
using Microsoft.EntityFrameworkCore;

namespace ProiectFinalDAW.Repositories.OrderRepository
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(Context C) : base(C)
        {

        }

        public Order GetByOrderNumber(int order_no)
        {
            return _table.Include(x => x.OrderDetails).ThenInclude(x => x.Product).FirstOrDefault(x => x.Order_Number.Equals(order_no));
        }
    }
}
