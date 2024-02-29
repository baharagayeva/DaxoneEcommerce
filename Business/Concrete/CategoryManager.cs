using AutoMapper;
using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;
        private readonly IValidator<Category> _validationRules;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDAL categoryDAL, IValidator<Category> validationRules, IMapper mapper)
        {
            _categoryDAL = categoryDAL;
            _validationRules = validationRules;
            _mapper = mapper;
        }
        public IDataResult<List<string>> Add(AddToCategoryDTO addToCategoryDTO)
        {
            Category category = _mapper.Map<Category>(addToCategoryDTO);
            var result = _validationRules.Validate(category);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _categoryDAL.Add(category);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(Category category)
        {
            _categoryDAL.Update(category);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<ListToCategoryDTO>> GetAll()
        {
            List<Category> categoryList = _categoryDAL.GetAll(x => x.Deleted == 0);
            categoryList.Sort((x, y) => y.ID.CompareTo(x.ID));

            return new SuccessDataResult<List<ListToCategoryDTO>>(_mapper.Map<List<ListToCategoryDTO>>(categoryList));

        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDAL.GetCategory(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(UpdateToCategoryDTO updateToCategoryDTO)
        {
            Category category = _mapper.Map<Category>(updateToCategoryDTO);
            var result = _validationRules.Validate(category);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _categoryDAL.Update(category);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
