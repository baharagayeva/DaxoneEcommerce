﻿using Core.DataAccess.Concrete;
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
    public class ProductStatusEFDal : RepositoryBase<ProductStatus, DaxoneDbContext>, IProductStatusDAL
    {
        private readonly DaxoneDbContext _context;

        public ProductStatusEFDal(DaxoneDbContext context)
        {
            _context = context;
        }
    }
}
