using Business.Abstract;
using Business.Concrete;
using Business.Validations;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Helpers.Constants;
using Core.Helpers.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdvertisementBannerController : ControllerBase
    {
        private readonly IAdvertisementBannerService _advertisementBannerService;

        public AdvertisementBannerController(IAdvertisementBannerService advertisementBannerService)
        {
            _advertisementBannerService = advertisementBannerService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _advertisementBannerService.GetAll();
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
        public async Task<AdvertisementBanner> GetAdmin(int id)
        {
            var data = _advertisementBannerService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add([FromForm] AdvertisementBanner advertisementBanner, IFormFile img)
        {

            var data = CloudinaryPost(img);
            advertisementBanner.ImgPath = data;
            var result = _advertisementBannerService.Add(advertisementBanner);

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
        public IActionResult Put([FromForm] AdvertisementBanner advertisementBanner,IFormFile image, int id)
        {

            var item = _advertisementBannerService.GetById(id).Data;
            if (image != null)
            {
                var oldlink = item.ImgPath;
                CloudinaryDelete(oldlink);
                var data = CloudinaryPost(image);
                advertisementBanner.ImgPath = data;
            }

            item.ImgPath = advertisementBanner.ImgPath;
            item.Discount = advertisementBanner.Discount;
            item.Description = advertisementBanner.Description;
            item.Title = advertisementBanner.Title;

            var result = _advertisementBannerService.Update(item);
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
            var advertisementBanner = _advertisementBannerService.GetById(id).Data;
            advertisementBanner.Deleted = advertisementBanner.ID;
            _advertisementBannerService.Delete(advertisementBanner);
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


