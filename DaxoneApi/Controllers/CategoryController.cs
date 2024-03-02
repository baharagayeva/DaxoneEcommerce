using Business.Abstract;
using Business.Concrete;
using Business.Validations;
using Core.Helpers.Constants;
using Core.Helpers.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _categoryService.GetAll();
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
        public async Task<Category> GetAdmin(int id)
        {
            var data = _categoryService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add(AddToCategoryDTO addToCategoryDTO)
        {
            Category category = new Category()
            {
                Name = addToCategoryDTO.Name,
            };
            var validator = new CategoryValidator();
            var validationResult = validator.Validate(category);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            _categoryService.Add(addToCategoryDTO);

            return Ok(CommonOperationMessages.DataAddedSuccessfully);
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateToCategoryDTO updateToCategoryDTO, int id)
        {

            updateToCategoryDTO.Id = id;
            _categoryService.Update(updateToCategoryDTO);
            return Ok(CommonOperationMessages.DataUpdatedSuccessfully);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id).Data;
            category.Deleted = category.ID;
            _categoryService.Delete(category);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }
    }
}
