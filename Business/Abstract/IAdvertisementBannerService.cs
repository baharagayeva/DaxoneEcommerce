using Core.Helpers.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdvertisementBannerService
    {
        IDataResult<List<string>> Add(AdvertisementBanner advertisementBanner);
        IResult Delete(AdvertisementBanner advertisementBanner);
        IDataResult<List<string>> Update(AdvertisementBanner advertisementBanner);
        IDataResult<AdvertisementBanner> GetById(int id);
        IDataResult<List<AdvertisementBanner>> GetAll();
    }
}
