using Business.Abstract;
using Business.Validations;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Core.Helpers.Constants;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult Add([FromForm] SeasonDiscount seasonDiscount, IFormFile img)
        {
            var data = CloudinaryPost(img);
            seasonDiscount.ImgPath = data;
            var result = _seasonDiscountService.Add(seasonDiscount);

            return Ok(result);
        }

        static string CloudinaryPost(IFormFile img)
        {
            string cloudName = "dkmr0x3ul";
            string apiKey = "482299463525874";
            string apiSecret = "Wss6cQtBxQBamETqlhQZKnCa8-c";
            string cloudinaryFolder = "Home/Daxone";

            var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
            var cloudinary = new Cloudinary(cloudinaryAccount);

            try
            {
                string uniqueFilename = Guid.NewGuid().ToString("N");
                string cloudinaryImagePath = $"{cloudinaryFolder}/{uniqueFilename}";
                string link = $"https://res.cloudinary.com/{cloudName}/{cloudinaryImagePath}";
                var uploadResult = UploadImageAndGetPath(cloudinary, img, cloudinaryImagePath);
                return link;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromForm] SeasonDiscount seasonDiscount, IFormFile image, int id)
        {

            var item = _seasonDiscountService.GetById(id).Data;
            if (image != null)
            {
                var oldlink = item.ImgPath;
                CloudinaryDelete(oldlink);
                var data = CloudinaryPost(image);
                seasonDiscount.ImgPath = data;
            }

            item.ImgPath = seasonDiscount.ImgPath;
            item.TitleDescription = seasonDiscount.TitleDescription;
            item.Description = seasonDiscount.Description;
            item.Title = seasonDiscount.Title;

            var result = _seasonDiscountService.Update(item);
            return Ok(result);
        }
        static string CloudinaryDelete(string image)
        {

            try
            {
                string cloudName = "dkmr0x3ul";
                string apiKey = "482299463525874";
                string apiSecret = "Wss6cQtBxQBamETqlhQZKnCa8-c";

                var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
                var cloudinary = new Cloudinary(cloudinaryAccount);

                var publicIds = new List<string>();
                publicIds.Add(image);

                DelResParams deleteParams = new DelResParams()
                {
                    PublicIds = publicIds
                };

                DelResResult result = cloudinary.DeleteResources(deleteParams);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "Images deleted successfully.";
                }
                else
                {
                    return "Failed to delete images. Error: " + result.Error.Message;
                }
            }


            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var seasonDiscount = _seasonDiscountService.GetById(id).Data;
            seasonDiscount.Deleted = seasonDiscount.ID;
            _seasonDiscountService.Delete(seasonDiscount);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }

        static string UploadImageAndGetPath(Cloudinary cloudinary, IFormFile image, string cloudinaryImagePath)
        {
            using (var stream = image.OpenReadStream())
            {
                cloudinaryImagePath = cloudinaryImagePath.TrimStart('/');

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    PublicId = cloudinaryImagePath,
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUri.ToString();
            }
        }
    }
}
