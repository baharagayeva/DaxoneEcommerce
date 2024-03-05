using Business.Abstract;
using Business.Validations;
using Core.Helpers.Constants;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.ProductStatusDTOs;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : ControllerBase
    {
        private readonly IProductStatusService _productStatusService;

        public ProductStatusController(IProductStatusService productStatusService)
        {
            _productStatusService = productStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _productStatusService.GetAll();
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
        public async Task<ProductStatus> GetAdmin(int id)
        {
            var data = _productStatusService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add(AddToProductStatusDTO addToProductStatusDTO)
        {
            ProductStatus productStatus = new ProductStatus()
            {
                New = addToProductStatusDTO.New,
                InStock = addToProductStatusDTO.InStock,
                StockOut = addToProductStatusDTO.StockOut,
            };
            var validator = new ProductStatusValidator();
            var validationResult = validator.Validate(productStatus);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            _productStatusService.Add(addToProductStatusDTO);

            return Ok(CommonOperationMessages.DataAddedSuccessfully);
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateToProductStatusDTO updateToProductStatusDTO, int id)
        {

            updateToProductStatusDTO.Id = id;
            _productStatusService.Update(updateToProductStatusDTO);
            return Ok(CommonOperationMessages.DataUpdatedSuccessfully);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productStatus = _productStatusService.GetById(id).Data;
            productStatus.Deleted = productStatus.ID;
            _productStatusService.Delete(productStatus);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }
    }
}
