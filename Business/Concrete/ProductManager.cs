using AutoMapper;
using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.ProductDTOs;
using Entities.Concrete.TableModels;
using Entities.Concrete.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        private readonly IValidator<Product> _validationRules;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public ProductManager(IProductDAL productDAL, IValidator<Product> validationRules, IMapper mapper,IUnitOfWorks unitOfWorks)
        {
            _productDAL = productDAL;
            _validationRules = validationRules;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
        }
        public IDataResult<List<string>> Add(ProductGetViewModel addToProductDTO)
        {
            Product product = _mapper.Map<Product>(addToProductDTO);
            var result = _validationRules.Validate(product);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _productDAL.AddWithProduct(addToProductDTO);
            _unitOfWorks.SaveChange();
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(Product product)
        {
            _productDAL.Update(product);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<Product>> GetAll()
        {
            List<Product> data = _productDAL.GetAllWithProduct();
            return new SuccessDataResult<List<Product>>(data);

            //List<Product> productList = _productDAL.GetAll(x => x.Deleted == 0);
            //productList.Sort((x, y) => y.ID.CompareTo(x.ID));
            //return new SuccessDataResult<List<ListToProductDTO>>(_mapper.Map<List<ListToProductDTO>>(productList));
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDAL.GetProductById(id));
        }

        public IDataResult<List<string>> Update(ProductUpdateViewModel updateToProductDTO)
        {
            Product product = _mapper.Map<Product>(updateToProductDTO);
            var result = _validationRules.Validate(product);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _productDAL.UpdateWithProduct(updateToProductDTO);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
