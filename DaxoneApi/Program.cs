using Business.Abstract;
using Business.Concrete;
using Business.Validations;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Mapper;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.TableModels;
using FluentValidation;

namespace DaxoneApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("cors", policy =>
                {
                    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                });
            });
            builder.Services.AddDbContext<DaxoneDbContext>()
                            .AddIdentity<User, Role>()
                            .AddEntityFrameworkStores<DaxoneDbContext>();
            builder.Services.AddAutoMapper(typeof(Automapper));

            builder.Services.AddScoped<ICategoryDAL, CategoryEFDal>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
            builder.Services.AddScoped<IAdvertisementBannerDAL, AdvertisementBannerEFDal>();
            builder.Services.AddScoped<IAdvertisementBannerService, AdvertisementBannerManager>();
            builder.Services.AddScoped<IValidator<AdvertisementBanner>, AdvertisementBannerValidator>();
            builder.Services.AddScoped<IColorDAL, ColorEFDal>();
            builder.Services.AddScoped<IColorService,  ColorManager>();
            builder.Services.AddScoped<IValidator<Color>, ColorValidator>();
            builder.Services.AddScoped<IProductDAL, ProductEFDal>();
            builder.Services.AddScoped<IProductService, ProductManager>();
            builder.Services.AddScoped<IValidator<Product>, ProductValidator>();


            var app = builder.Build();

         
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("cors");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}