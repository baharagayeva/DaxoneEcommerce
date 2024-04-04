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
    public class ColorEFDal : RepositoryBase<Color, DaxoneDbContext>, IColorDAL
    {
        private readonly DaxoneDbContext _context;

        public ColorEFDal(DaxoneDbContext context)
        {
            _context = context;
        }

    }
}
