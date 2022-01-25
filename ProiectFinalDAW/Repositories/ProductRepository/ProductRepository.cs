using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;
using Microsoft.EntityFrameworkCore;

namespace ProiectFinalDAW.Repositories.ProductRepository
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(Context C) : base(C)
        {

        }

        public Product GetByProductBarCode(int BarCode)
        {
            return _table.Include(x => x.Category).FirstOrDefault(x => x.BarCode.Equals(BarCode));
        }
    }
}
