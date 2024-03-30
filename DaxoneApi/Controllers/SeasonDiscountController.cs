using Business.Abstract;
using Business.Validations;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Core.Helpers.Constants;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;
using Business.Cloudinaries;
using Microsoft.AspNetCore.Authorization;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SeasonDiscountController : ControllerBase
    {
        private readonly ISeasonDiscountService _seasonDiscountService;

        public SeasonDiscountController(ISeasonDiscountService seasonDiscountService)
        {
            _seasonDiscountService = seasonDiscountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _seasonDiscountService.GetAll();
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
        public async Task<SeasonDiscount> GetAdmin(int id)
        {
            var data = _seasonDiscountService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add([FromForm] SeasonDiscount seasonDiscount)
        {
            var data = CloudinarySettings.CloudinaryPost(seasonDiscount.Image);
            seasonDiscount.ImgPath = data;
            var result = _seasonDiscountService.Add(seasonDiscount);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromForm] SeasonDiscount seasonDiscount, int id)
        {

            var item = _seasonDiscountService.GetById(id).Data;
            if (seasonDiscount.Image != null)
            {
                var oldlink = item.ImgPath;
                CloudinarySettings.CloudinaryDelete(oldlink);
                var data = CloudinarySettings.CloudinaryPost(seasonDiscount.Image);
                seasonDiscount.ImgPath = data;
            }

            item.ImgPath = seasonDiscount.ImgPath;
            item.TitleDescription = seasonDiscount.TitleDescription;
            item.Description = seasonDiscount.Description;
            item.Title = seasonDiscount.Title;

            var result = _seasonDiscountService.Update(item);
            return Ok(result);
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var seasonDiscount = _seasonDiscountService.GetById(id).Data;
            seasonDiscount.Deleted = seasonDiscount.ID;
            _seasonDiscountService.Delete(seasonDiscount);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }

    }
}
