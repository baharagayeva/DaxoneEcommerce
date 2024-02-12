using Business.Concrete;
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
        public CategoryManager CategoryManager = new CategoryManager();

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
    }
}
