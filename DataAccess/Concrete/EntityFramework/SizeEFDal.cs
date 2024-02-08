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
    public class SizeEFDal : RepositoryBase<Size, DaxoneDbContext>, ISizeDAL
    {
        private readonly DaxoneDbContext _context;

        public SizeEFDal(DaxoneDbContext context)
        {
            _context = context;
        }

        public Size GetAll(Expression<Func<Size, bool>> predicate = null)
        {

            return predicate is null
                  ?
                   _context.Set<Size>().Include(x => x.Products).FirstOrDefault()
                  :
                  _context.Set<Size>().Include(x => x.Products).FirstOrDefault(predicate);
        }
    }
}
