using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class CategoryEFDal : RepositoryBase<Category, DaxoneDbContext>, ICategoryDAL
    {
        private readonly DaxoneDbContext _context;

        public CategoryEFDal(DaxoneDbContext context)
        {
            _context = context;
        }

        public Category GetAllDATA(Expression<Func<Category, bool>> predicate = null)
        {

            return predicate is null
                  ?
                   _context.Set<Category>().Include(x => x.Products).Include(x => x.SubCategories).FirstOrDefault()
                  :
                  _context.Set<Category>().Include(x => x.Products).Include(x => x.SubCategories).FirstOrDefault(predicate);
        }
    }
}
