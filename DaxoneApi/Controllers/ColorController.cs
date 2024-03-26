using Business.Abstract;
using Business.Validations;
using Core.Helpers.Constants;
using Core.Helpers.Results.Concrete;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.TableModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _colorService.GetAll();
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
        public async Task<Color> GetAdmin(int id)
        {
            var data = _colorService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add(AddToColorDTO addToColorDTO)
        {
            var result = _colorService.Add(addToColorDTO);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateToColorDTO updateToColorDTO, int id)
        {

            updateToColorDTO.Id = id;
            var result = _colorService.Update(updateToColorDTO);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var color = _colorService.GetById(id).Data;
            color.Deleted = color.ID;
            _colorService.Delete(color);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }
    }

}
