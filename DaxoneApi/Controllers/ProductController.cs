using Business.Abstract;
using Business.Validations;
using Core.Helpers.Constants;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.ProductDTOs;
using FluentValidation;
using Business.Cloudinaries;
using Entities.Concrete.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<Product> GetAdmin(int id)
        {
            var data = _productService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add([FromForm] ProductGetViewModel addToProductDTO)
        {
            var data = CloudinarySettings.CloudinaryPost(addToProductDTO.Image);
            addToProductDTO.ImgPath = data;
            var result = _productService.Add(addToProductDTO);

            return Ok(result);
        }

        

        [HttpPut("{id}")]
        public IActionResult Put([FromForm] ProductUpdateViewModel updateToProductDTO, int id)
        {

            var item = _productService.GetById(id).Data;
            if (updateToProductDTO.Image != null)
            {
                var oldlink = item.ImgPath;
                CloudinarySettings.CloudinaryDelete(oldlink);
                var data = CloudinarySettings.CloudinaryPost(updateToProductDTO.Image);
                updateToProductDTO.ImgPath = data;
            }

            item.ID = id;
            item.Name = updateToProductDTO.Name;
            item.Description = updateToProductDTO.Description;
            item.CategoryID = updateToProductDTO.CategoryID;
            item.IsSale = updateToProductDTO.IsSale;
            item.Price = updateToProductDTO.Price;
            item.SalePrice = updateToProductDTO.SalePrice;
            item.ImgPath = updateToProductDTO.ImgPath;
            item.Model = updateToProductDTO.Model;
            item.StockCount = updateToProductDTO.StockCount;

            var result = _productService.Update(updateToProductDTO);
            return Ok(result);
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id).Data;
            product.Deleted = product.ID;
            _productService.Delete(product);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }

       
    }
}
