using Business.Abstract;
using Business.Cloudinaries;
using Business.Concrete;
using Business.Validations;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        public IActionResult Add([FromForm] AdvertisementBanner advertisementBanner)
        {

            var data = CloudinarySettings.CloudinaryPost(advertisementBanner.Image);
            advertisementBanner.ImgPath = data;
            var result = _advertisementBannerService.Add(advertisementBanner);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromForm] AdvertisementBanner advertisementBanner, int id)
        {

            var item = _advertisementBannerService.GetById(id).Data;
            if (advertisementBanner.Image != null)
            {
                var oldlink = item.ImgPath;
                CloudinarySettings.CloudinaryDelete(oldlink);
                var data = CloudinarySettings.CloudinaryPost(advertisementBanner.Image);
                advertisementBanner.ImgPath = data;
            }

            item.ImgPath = advertisementBanner.ImgPath;
            item.Discount = advertisementBanner.Discount;
            item.Description = advertisementBanner.Description;
            item.Title = advertisementBanner.Title;

            var result = _advertisementBannerService.Update(item);
            return Ok(result);
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var advertisementBanner = _advertisementBannerService.GetById(id).Data;
            advertisementBanner.Deleted = advertisementBanner.ID;
            _advertisementBannerService.Delete(advertisementBanner);
            return Ok(CommonOperationMessages.DataDeletedSuccessfully);
        }


    }
}


