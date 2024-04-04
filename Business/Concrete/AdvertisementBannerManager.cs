using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.TableModels;
using FluentValidation;
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
        private readonly IValidator<AdvertisementBanner> _validationRules;
        public AdvertisementBannerManager(IAdvertisementBannerDAL advertisementBannerDAL, IValidator<AdvertisementBanner> validationRules)
        {
            _advertisementBannerDAL = advertisementBannerDAL;
            _validationRules = validationRules;
        }
        public IDataResult<List<string>> Add(AdvertisementBanner advertisementBanner)
        {
            var result = _validationRules.Validate(advertisementBanner);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
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
            var data = _advertisementBannerDAL.GetAll(x => x.Deleted == 0);
            data.Reverse();
            return new SuccessDataResult<List<AdvertisementBanner>>(data);
        }

        public IDataResult<AdvertisementBanner> GetById(int id)
        {
            return new SuccessDataResult<AdvertisementBanner>(_advertisementBannerDAL.Get(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(AdvertisementBanner advertisementBanner)
        {
            var result = _validationRules.Validate(advertisementBanner);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _advertisementBannerDAL.Update(advertisementBanner);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
