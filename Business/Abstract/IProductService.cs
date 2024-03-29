using Core.Helpers.Results.Abstract;
using Entities.Concrete.DTOs.ProductDTOs;
using Entities.Concrete.TableModels;
using Entities.Concrete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<string>> Add(ProductGetViewModel addToProductDTO);
        IResult Delete(Product product);
        IDataResult<List<string>> Update(ProductUpdateViewModel updateToProductDTO);
        IDataResult<Product> GetById(int id);
        IDataResult<List<Product>> GetAll();
    }
}
