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
    public class SubCategoryEFDal : RepositoryBase<SubCategory, DaxoneDbContext>, ISubCategoryDAL
    {
        private readonly DaxoneDbContext _context;

        public SubCategoryEFDal(DaxoneDbContext context)
        {
            _context = context;
        }

        public List<SubCategory> GetAll(Expression<Func<SubCategory, bool>> predicate = null)
        {
            return predicate is null
            ?
                _context.Set<SubCategory>().Include(x => x.Category).ToList()
            :
                _context.Set<SubCategory>().Include(x => x.Category).Where(predicate).ToList(); ;
        }
    }
}
