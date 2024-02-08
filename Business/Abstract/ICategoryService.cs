using Core.Helpers.Results.Abstract;
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
        IDataResult<List<string>> Add(Category category);
        IResult Delete(Category category);
        IDataResult<List<string>> Update(Category category);
        IDataResult<Category> GetById(int id);
        IDataResult<List<Category>> GetAll();
    }
}
