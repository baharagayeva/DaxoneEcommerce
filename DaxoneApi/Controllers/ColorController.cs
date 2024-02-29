using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Concrete;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpGet]
        public async Task<List<Color>> Get()
        {

            var data = _colorService.GetAll();
            return data.Data;
        }
        [HttpGet("{id}")]
        public async Task<Color> GetAdmin(int id)
        {
            var data = _colorService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public Result Add(Color color)
        {
            _colorService.Add(color);
            return new SuccessResult(CommonOperationMessages.DataAddedSuccessfully);
        }

        [HttpPut("{id}")]
        public Result Put(Color color, int id)
        {

            var data = _colorService.GetById(id).Data;
            data.Name = color.Name;
            data.ColorCode = color.ColorCode;
            _colorService.Update(data);
            return new SuccessResult(CommonOperationMessages.DataUpdatedSuccessfully);
        }

        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            var color = _colorService.GetById(id).Data;
            color.Deleted = color.ID;
            _colorService.Delete(color);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }
    }
}
