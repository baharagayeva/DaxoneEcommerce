using AutoMapper;
using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs.ProductDTOs;
using Entities.Concrete.DTOs.ProductStatusDTOs;
using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductStatusManager : IProductStatusService
    {
        private readonly IProductStatusDAL _productStatusDAL;
        private readonly IValidator<ProductStatus> _validationRules;
        private readonly IMapper _mapper;

        public ProductStatusManager(IProductStatusDAL productStatusDAL, IValidator<ProductStatus> validationRules, IMapper mapper)
        {
            _productStatusDAL = productStatusDAL;
            _validationRules = validationRules;
            _mapper = mapper;
        }
        public IDataResult<List<string>> Add(AddToProductStatusDTO addToProductStatusDTO)
        {
            ProductStatus productStatus = _mapper.Map<ProductStatus>(addToProductStatusDTO);
            var result = _validationRules.Validate(productStatus);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _productStatusDAL.Add(productStatus);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(ProductStatus productStatus)
        {
            _productStatusDAL.Update(productStatus);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<ListToProductStatusDTO>> GetAll()
        {
            List<ProductStatus> productStatusList = _productStatusDAL.GetAll(x => x.Deleted == 0);
            productStatusList.Sort((x, y) => y.ID.CompareTo(x.ID));
            return new SuccessDataResult<List<ListToProductStatusDTO>>(_mapper.Map<List<ListToProductStatusDTO>>(productStatusList));
        }

        public IDataResult<ProductStatus> GetById(int id)
        {
            return new SuccessDataResult<ProductStatus>(_productStatusDAL.Get(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(UpdateToProductStatusDTO updateToProductStatusDTO)
        {
            ProductStatus productStatus = _mapper.Map<ProductStatus>(updateToProductStatusDTO);
            var result = _validationRules.Validate(productStatus);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _productStatusDAL.Update(productStatus);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
