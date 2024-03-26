using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class AdvertisementBannerEFDal : RepositoryBase<AdvertisementBanner, DaxoneDbContext>, IAdvertisementBannerDAL
    {
        private readonly DaxoneDbContext _context;

        public AdvertisementBannerEFDal(DaxoneDbContext context) 
        {
            _context = context;
        }

    }
}
