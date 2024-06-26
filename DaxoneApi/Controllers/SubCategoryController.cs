﻿using Business.Abstract;
using Business.Validations;
using Core.Helpers.Constants;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.SubCategoryDTOs;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
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
            var result = _subCategoryService.Add(addToSubCategoryDTO);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateToSubCategoryDTO updateToSubCategoryDTO, int id)
        {

            updateToSubCategoryDTO.Id = id;
            var result = _subCategoryService.Update(updateToSubCategoryDTO);
            return Ok(result);
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
