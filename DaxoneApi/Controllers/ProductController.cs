using Business.Abstract;
using Business.Validations;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Core.Helpers.Constants;
using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.ProductDTOs;
using FluentValidation;

namespace DaxoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _productService.GetAll();
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
        public async Task<Product> GetAdmin(int id)
        {
            var data = _productService.GetById(id);
            return data.Data;
        }

        [HttpPost]
        public IActionResult Add([FromForm] AddToProductDTO addToProductDTO, IFormFile img)
        {
            var data = CloudinaryPost(img);
            addToProductDTO.ImgPath = data;
            var result = _productService.Add(addToProductDTO);

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
        public IActionResult Put([FromForm] UpdateToProductDTO updateToProductDTO, IFormFile image, int id)
        {

            var item = _productService.GetById(id).Data;
            if (image != null)
            {
                var oldlink = item.ImgPath;
                CloudinaryDelete(oldlink);
                var data = CloudinaryPost(image);
                updateToProductDTO.ImgPath = data;
            }

            item.ID = id;
            item.Name = updateToProductDTO.Name;
            item.Description = updateToProductDTO.Description;
            item.CategoryID = updateToProductDTO.CategoryID;
            item.IsSale = updateToProductDTO.IsSale;
            item.Price = updateToProductDTO.Price;
            item.SalePrice = updateToProductDTO.SalePrice;
            item.ImgPath = updateToProductDTO.ImgPath;
            item.Model = updateToProductDTO.Model;
            item.StockCount = updateToProductDTO.StockCount;

            var result = _productService.Update(updateToProductDTO);
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
            var product = _productService.GetById(id).Data;
            product.Deleted = product.ID;
            _productService.Delete(product);
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
