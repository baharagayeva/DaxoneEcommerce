using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdvertisementBannerManager : IAdvertisementBannerService
    {
        private readonly IAdvertisementBannerDAL _advertisementBannerDAL;
        public AdvertisementBannerManager(IAdvertisementBannerDAL advertisementBannerDAL)
        {
            _advertisementBannerDAL = advertisementBannerDAL;
        }
        public IDataResult<List<string>> Add(AdvertisementBanner advertisementBanner)
        {
            _advertisementBannerDAL.Add(advertisementBanner);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(AdvertisementBanner advertisementBanner)
        {
            _advertisementBannerDAL.Update(advertisementBanner);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<AdvertisementBanner>> GetAll()
        {
            return new SuccessDataResult<List<AdvertisementBanner>>(_advertisementBannerDAL.GetAll(x => x.Deleted == 0));
        }

        public IDataResult<AdvertisementBanner> GetById(int id)
        {
            return new SuccessDataResult<AdvertisementBanner>(_advertisementBannerDAL.Get(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(AdvertisementBanner advertisementBanner)
        {
            _advertisementBannerDAL.Update(advertisementBanner);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
