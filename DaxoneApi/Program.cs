using Business.Abstract;
using Business.Concrete;
using Business.Validations;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Mapper;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.TableModels;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace DaxoneApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Daxone")
                .AddEntityFrameworkStores<DaxoneAuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Daxone API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = "Oauth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

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

            builder.Services.AddDbContext<DaxoneAuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbAuth"));
            });
            builder.Services.AddAutoMapper(typeof(Automapper));

            builder.Services.AddScoped<ICategoryDAL, CategoryEFDal>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
            builder.Services.AddScoped<IAdvertisementBannerDAL, AdvertisementBannerEFDal>();
            builder.Services.AddScoped<IAdvertisementBannerService, AdvertisementBannerManager>();
            builder.Services.AddScoped<IValidator<AdvertisementBanner>, AdvertisementBannerValidator>();
            builder.Services.AddScoped<IColorDAL, ColorEFDal>();
            builder.Services.AddScoped<IColorService, ColorManager>();
            builder.Services.AddScoped<IValidator<Color>, ColorValidator>();
            builder.Services.AddScoped<IProductDAL, ProductEFDal>();
            builder.Services.AddScoped<IProductService, ProductManager>();
            builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
            builder.Services.AddScoped<IProductStatusDAL, ProductStatusEFDal>();
            builder.Services.AddScoped<IProductStatusService, ProductStatusManager>();
            builder.Services.AddScoped<IValidator<ProductStatus>, ProductStatusValidator>();
            builder.Services.AddScoped<ISeasonDiscountDAL, SeasonDiscountEFDal>();
            builder.Services.AddScoped<ISeasonDiscountService, SeasonDiscountManager>();
            builder.Services.AddScoped<IValidator<SeasonDiscount>, SeasonDiscountValidator>();
            builder.Services.AddScoped<ISizeDAL, SizeEFDal>();
            builder.Services.AddScoped<ISizeService, SizeManager>();
            builder.Services.AddScoped<IValidator<Size>, SizeValidator>();
            builder.Services.AddScoped<ISubCategoryDAL, SubCategoryEFDal>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryManager>();
            builder.Services.AddScoped<IValidator<SubCategory>, SubCategoryValidator>();
            builder.Services.AddScoped<ITokenService, TokenManager>();


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Daxone API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("cors");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}