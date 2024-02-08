using Core.Helpers.Results.Abstract;
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
        IDataResult<List<string>> Add(SubCategory subCategory);
        IResult Delete(SubCategory subCategory);
        IDataResult<List<string>> Update(SubCategory subCategory);
        IDataResult<SubCategory> GetById(int id);
        IDataResult<List<SubCategory>> GetAll();
    }
}
