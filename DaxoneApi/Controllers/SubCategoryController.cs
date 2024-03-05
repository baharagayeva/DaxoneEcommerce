using Business.Abstract;
using Business.Validations;
using Core.Helpers.Constants;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.SubCategoryDTOs;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _subCategoryService.GetAll();
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
        public async Task<SubCategory> GetAdmin(int id)
        {
            var data = _subCategoryService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add(AddToSubCategoryDTO addToSubCategoryDTO)
        {
            SubCategory subCategory = new SubCategory()
            {
                Name = addToSubCategoryDTO.Name,
                CategoryID = addToSubCategoryDTO.CategoryID,
            };
            var validator = new SubCategoryValidator();
            var validationResult = validator.Validate(subCategory);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            _subCategoryService.Add(addToSubCategoryDTO);

            return Ok(CommonOperationMessages.DataAddedSuccessfully);
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateToSubCategoryDTO updateToSubCategoryDTO, int id)
        {

            updateToSubCategoryDTO.Id = id;
            _subCategoryService.Update(updateToSubCategoryDTO);
            return Ok(CommonOperationMessages.DataUpdatedSuccessfully);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var subCategory = _subCategoryService.GetById(id).Data;
            subCategory.Deleted = subCategory.ID;
            _subCategoryService.Delete(subCategory);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }
    }
}
