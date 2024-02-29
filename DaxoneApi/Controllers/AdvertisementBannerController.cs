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
    public class AdvertisementBannerController : ControllerBase
    {
        private readonly IAdvertisementBannerService _advertisementBannerService;

        public AdvertisementBannerController(IAdvertisementBannerService advertisementBannerService)
        {
            _advertisementBannerService = advertisementBannerService;

        }

        [HttpGet]
        public async Task<List<AdvertisementBanner>> Get()
        {
       
            var data = _advertisementBannerService.GetAll();
            return data.Data;
        }
        [HttpGet("{id}")]
        public async Task<AdvertisementBanner> GetAdmin(int id)
        {
            var data = _advertisementBannerService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add(AdvertisementBanner advertisementBanner)
        {
            var validator = new AdvertisementBannerValidator();
            var validationResult = validator.Validate(advertisementBanner);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            _advertisementBannerService.Add(advertisementBanner);

            return Ok(CommonOperationMessages.DataAddedSuccessfully);
        }


        //[HttpPost]
        //public Result Add([FromForm] AdvertisementBanner advertisementBanner,IFormFile img)
        //{
        //    string cloudName = "dkmr0x3ul";
        //    string apiKey = "482299463525874";
        //    string apiSecret = "Wss6cQtBxQBamETqlhQZKnCa8-c";
        //    string cloudinaryFolder = "Home/Daxone";

        //    var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
        //    var cloudinary = new Cloudinary(cloudinaryAccount);
        //    string uniqueFilename = Guid.NewGuid().ToString("N");
        //    string cloudinaryImagePath = $"{cloudinaryFolder}/{uniqueFilename}";

        //    var uploadResult = UploadImageAndGetPath(cloudinary, img, cloudinaryImagePath);
        //    advertisementBanner.ImgPath=$"https://res.cloudinary.com/dkmr0x3ul/image/upload/v1708592233/{cloudinaryImagePath}";

        //    _advertisementBannerService.Add(advertisementBanner);

        //    return new SuccessResult(CommonOperationMessages.DataAddedSuccessfully);
        //}

        [HttpPut("{id}")]
        public Result Put(AdvertisementBanner advertisementBanner,int id)
        {

            var data= _advertisementBannerService.GetById(id).Data;
            data.ImgPath = advertisementBanner.ImgPath;
            data.Discount= advertisementBanner.Discount;
            data.Description= advertisementBanner.Description;
            data.Title= advertisementBanner.Title;
            _advertisementBannerService.Update(data);
            return new SuccessResult(CommonOperationMessages.DataUpdatedSuccessfully);
        }

        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            var advertisementBanner = _advertisementBannerService.GetById(id).Data;
            advertisementBanner.Deleted = advertisementBanner.ID;
            _advertisementBannerService.Delete(advertisementBanner);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
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


