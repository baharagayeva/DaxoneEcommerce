using Core.Helpers.Results.Abstract;
using Entities.Concrete.DTOs.SubCategoryDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISubCategoryService
    {
        IDataResult<List<string>> Add(AddToSubCategoryDTO addToSubCategoryDTO);
        IResult Delete(SubCategory subCategory);
        IDataResult<List<string>> Update(UpdateToSubCategoryDTO updateToSubCategoryDTO);
        IDataResult<SubCategory> GetById(int id);
        IDataResult<List<ListToSubCategoryDTO>> GetAll();
    }
}
