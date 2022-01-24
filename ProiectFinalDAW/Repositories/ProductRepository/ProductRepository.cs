using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;

namespace ProiectFinalDAW.Repositories.ProductRepository
{
    public class ProductRepository:GenericRepository<Product>
    {
        public ProductRepository(Context C) : base(C)
        {

        }
    }
}
