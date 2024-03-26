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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
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
        //[Authorize(Roles = "Reader")]
        public async Task<Category> GetAdmin(int id)
        {
            var data = _categoryService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public IActionResult Add(AddToCategoryDTO addToCategoryDTO)
        {
            var result = _categoryService.Add(addToCategoryDTO);
            return Ok(result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Writer")]
        public IActionResult Put(UpdateToCategoryDTO updateToCategoryDTO, int id)
        {

            updateToCategoryDTO.Id = id;
            var result = _categoryService.Update(updateToCategoryDTO);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Writer")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id).Data;
            category.Deleted = category.ID;
            _categoryService.Delete(category);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }
    }
}
