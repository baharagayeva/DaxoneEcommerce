using Business.Abstract;
using Business.Validations;
using Core.Helpers.Constants;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.DTOs.SizeDTOs;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _sizeService.GetAll();
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
        public async Task<Size> GetAdmin(int id)
        {
            var data = _sizeService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add(AddToSizeDTO addToSizeDTO)
        {
            Size size = new Size()
            {
                Name = addToSizeDTO.Name,
            };
            var validator = new SizeValidator();
            var validationResult = validator.Validate(size);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            _sizeService.Add(addToSizeDTO);

            return Ok(CommonOperationMessages.DataAddedSuccessfully);
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateToSizeDTO updateToSizeDTO, int id)
        {

            updateToSizeDTO.Id = id;
            _sizeService.Update(updateToSizeDTO);
            return Ok(CommonOperationMessages.DataUpdatedSuccessfully);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var size = _sizeService.GetById(id).Data;
            size.Deleted = size.ID;
            _sizeService.Delete(size);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }
    }
}
