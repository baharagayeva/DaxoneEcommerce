using Business.Concrete;
using Business.Validations;
using Core.Helpers.Constants;
using Core.Helpers.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
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
        public CategoryManager CategoryManager = new CategoryManager(new CategoryEFDal(new DaxoneDbContext()),new CategoryValidator());

        [HttpGet]
        public async Task<List<Category>> Get()
        {
           var data= CategoryManager.GetAll();
            return data.Data;
        }
        [Authorize]
        [HttpGet("Admin")]
        public async Task<List<Category>> GetAdmin()
        {
           var data= CategoryManager.GetAll();
            return data.Data;
        }

        [HttpPost]
        public Result Add(Category category)
        {
            CategoryManager.Add(category);
            return new SuccessResult(CommonOperationMessages.DataAddedSuccessfully);
        }

        [HttpPut]
        public Result Put(Category category)
        {
            CategoryManager.Update(category);
            return new SuccessResult(CommonOperationMessages.DataAddedSuccessfully);
        }

        [HttpDelete]
        public Result Delete(int id)
        {
            var category = CategoryManager.GetById(id);
            CategoryManager.Delete(category.Data);
            return new SuccessResult(CommonOperationMessages.DataAddedSuccessfully);
        }
    }
}
