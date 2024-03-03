using Core.Helpers.Results.Abstract;
using Entities.Concrete.DTOs.ProductDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<string>> Add(AddToProductDTO addToProductDTO);
        IResult Delete(Product product);
        IDataResult<List<string>> Update(UpdateToProductDTO updateToProductDTO);
        IDataResult<Product> GetById(int id);
        IDataResult<List<ListToProductDTO>> GetAll();
    }
}
