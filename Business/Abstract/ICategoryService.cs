using Core.Helpers.Results.Abstract;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<string>> Add(AddToCategoryDTO addToCategoryDTO);
        IResult Delete(Category category);
        IDataResult<List<string>> Update(UpdateToCategoryDTO updateToCategoryDTO);
        IDataResult<Category> GetById(int id);
        IDataResult<List<ListToCategoryDTO>> GetAll();
    }
}
