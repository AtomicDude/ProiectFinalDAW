using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;
using ProiectFinalDAW.Repositories.GenericRepository;

namespace ProiectFinalDAW.Repositories.CategoryRepository
{
    public class CategoryRepository:GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context C) : base(C)
        {

        }

        public Category GetByCategory(string name)
        {
            return _table.FirstOrDefault(x => x.Category_Name.Equals(name));
        }
    }
}
