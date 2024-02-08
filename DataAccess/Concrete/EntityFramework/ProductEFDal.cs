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
    public class ProductEFDal : RepositoryBase<Product, DaxoneDbContext>, IProductDAL
    {
        private readonly DaxoneDbContext _context;

        public ProductEFDal(DaxoneDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> predicate = null)
        {
            return predicate is null
            ?
                _context.Set<Product>().Include(x => x.Colors).Include(x => x.Sizes).Include(x => x.Category).Include(x => x.ProductStatuses).ToList()
            :
                _context.Set<Product>().Include(x => x.Colors).Include(x => x.Sizes).Include(x => x.Category).Include(x => x.ProductStatuses).Where(predicate).ToList(); ;
        }

    }
}
