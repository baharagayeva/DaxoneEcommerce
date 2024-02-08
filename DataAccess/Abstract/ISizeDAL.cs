using Core.DataAccess.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISizeDAL : IRepository<Size>
    {
        Size GetAll(Expression<Func<Size, bool>> predicate = null);
    }
}
