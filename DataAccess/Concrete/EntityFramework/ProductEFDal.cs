using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.TableModels;
using Entities.Concrete.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductEFDal : RepositoryBase<Product, DaxoneDbContext>, IProductDAL
    {
        private readonly DaxoneDbContext _context;

        public ProductEFDal(DaxoneDbContext context)
        {
            _context = context;
        }

        public void AddWithProduct(ProductGetViewModel producGetViewModel)
        {
            var Product = new Product
            {
                Name = producGetViewModel.Name,
                CategoryID = producGetViewModel.CategoryID,
                Description = producGetViewModel.Description,
                ImgPath = producGetViewModel.ImgPath,
                SalePrice = producGetViewModel.SalePrice,
                Price = producGetViewModel.Price,
                IsSale = producGetViewModel.IsSale,
                Model = producGetViewModel.Model,
                StockCount = producGetViewModel.StockCount,

            };
            foreach (var item in producGetViewModel.SizeIds)
            {
                Product.ProductSizes.Add(new ProductSize()
                {
                    Product = Product,
                    SizeID = item
                });

                Product.ProductColors.Add(new ProductColor()
                {
                    Product = Product,
                    ColorID = item
                });

                Product.ProductProductStatuses.Add(new ProductProductStatus()
                {
                    Product = Product,
                    ProductStatusID = item
                });



            }
            _context.Products.Add(Product);
        }

        public List<Product> GetAllWithProduct()
        {
            var result = (from p in _context.Products
                          where p.Deleted == 0
                          select new Product
                          {
                              CategoryID = p.CategoryID,
                              Name = p.Name,
                              Description = p.Description,
                              Deleted = p.Deleted,
                              ID = p.ID,
                              ImgPath = p.ImgPath,
                              IsSale = p.IsSale,
                              SalePrice = p.SalePrice,
                              Price = p.Price,
                              Model = p.Model,
                              StockCount = p.StockCount,
                              Category = (from c in _context.Categories
                                          where c.ID == p.CategoryID
                                          select new Category
                                          {
                                              ID = c.ID,
                                              Name = c.Name
                                          }).FirstOrDefault(),
                              ProductSizes = (from ps in _context.ProductSizes
                                              join size in _context.Sizes on ps.SizeID equals size.ID
                                              where ps.ProductID == p.ID
                                              select new ProductSize
                                              {
                                                  Size = new Size
                                                  {
                                                      ID = size.ID,
                                                      Name = size.Name
                                                  }
                                              }).ToList(),
                              ProductColors = (from pc in _context.ProductColors
                                               join color in _context.Colors on pc.ColorID equals color.ID
                                               where pc.ProductID == p.ID
                                               select new ProductColor
                                               {
                                                   Color = new Color
                                                   {
                                                       ID = color.ID,
                                                       Name = color.Name,
                                                       ColorCode = color.ColorCode,
                                                   }
                                               }).ToList(),
                              ProductProductStatuses = (from pps in _context.ProductProductStatuses
                                                        join productStatus in _context.ProductStatuses on pps.ProductStatusID equals productStatus.ID
                                                        where pps.ProductID == p.ID
                                                        select new ProductProductStatus
                                                        {
                                                            ProductStatus = new ProductStatus
                                                            {
                                                                ID = productStatus.ID,
                                                                IsNew = productStatus.IsNew,
                                                                IsStock = productStatus.IsStock,
                                                                IsStockOut = productStatus.IsStockOut,
                                                            }
                                                        }).ToList()
                          }).ToList();

            return result;

        }

        public Product GetProductById(int id)
        {
            var result = (from ps in _context.ProductSizes
                          join p in _context.Products
                          on ps.ProductID equals p.ID
                          join s in _context.Sizes
                          on ps.SizeID equals s.ID
                          where p.ID == id
                          select new Product
                          {
                              CategoryID = p.CategoryID,
                              Name = p.Name,
                              Description = p.Description,
                              Deleted = p.Deleted,
                              ID = p.ID,
                              ImgPath = p.ImgPath,
                              IsSale = p.IsSale,
                              SalePrice = p.SalePrice,
                              Price = p.Price,
                              Model = p.Model,
                              StockCount = p.StockCount,
                              Category = (from c in _context.Categories
                                          where c.ID == p.CategoryID
                                          select new Category
                                          {
                                              ID = c.ID,
                                              Name = c.Name
                                          }).FirstOrDefault(),

                              ProductSizes = (from ps in _context.ProductSizes
                                              where ps.ProductID == p.ID
                                              select new ProductSize
                                              {
                                                  Size = (from size in _context.Sizes
                                                          where size.ID == ps.SizeID
                                                          select new Size
                                                          {
                                                              ID = size.ID,
                                                              Deleted = size.Deleted,
                                                              Name = size.Name,
                                                          }).First()
                                              }).ToList()
                                ,
                              ProductColors = (from pc in _context.ProductColors
                                               where pc.ProductID == p.ID
                                               select new ProductColor
                                               {
                                                   Color = (from color in _context.Colors
                                                            where color.ID == pc.ColorID
                                                            select new Color
                                                            {
                                                                ID = color.ID,
                                                                Deleted = color.Deleted,
                                                                Name = color.Name,
                                                                ColorCode = color.ColorCode,
                                                            }).First()
                                               }).ToList()
                                                ,
                              ProductProductStatuses = (from pps in _context.ProductProductStatuses
                                             where pps.ProductID == p.ID
                                             select new ProductProductStatus
                                             {
                                                 ProductStatus = (from productStatus in _context.ProductStatuses
                                                        where productStatus.ID == pps.ProductStatusID
                                                        select new ProductStatus
                                                        {
                                                            ID = productStatus.ID,
                                                            Deleted = productStatus.Deleted,
                                                            IsNew = productStatus.IsNew,
                                                            IsStock = productStatus.IsStock,
                                                            IsStockOut = productStatus.IsStockOut,
                                                        }).First()
                                             }).ToList()
                                
                          }).FirstOrDefault();

            return result;
        }

        public void UpdateWithProduct(ProductUpdateViewModel productUpdateViewModel)
        {
            var product = _context.Products
                 .Include(x => x.ProductSizes)
                 .ThenInclude(x => x.Size)
                 .Include(x => x.ProductColors)
                 .ThenInclude(x => x.Color)
                 .FirstOrDefault(x => x.ID == productUpdateViewModel.ID);

            product.Name = productUpdateViewModel.Name;
            var existingIds = product.ProductSizes.Select(x => x.ID).ToList();
            var selectIds = productUpdateViewModel.SizeIds.ToList();
            var toAdd = selectIds.Except(existingIds).ToList();
            var toRemove = existingIds.Except(selectIds).ToList();
            product.ProductSizes = product.ProductSizes.Where(x => x.SizeID.HasValue && !toRemove.Contains((x.SizeID.Value)).Equals(toAdd)).ToList();
            //ProductColor
            var existingIds1 = product.ProductColors.Select(x => x.ID).ToList();
            var selectIds1 = productUpdateViewModel.ColorIds.ToList();
            var toAdd1 = selectIds1.Except(existingIds1).ToList();
            var toRemove1 = existingIds1.Except(selectIds1).ToList();
            product.ProductColors = product.ProductColors.Where(x => x.ColorID.HasValue && !toRemove1.Contains((x.ColorID.Value)).Equals(toAdd1)).ToList();

            //ProductProductStatus
            var existingIds2 = product.ProductProductStatuses.Select(x => x.ID).ToList();
            var selectIds2 = productUpdateViewModel.ProductStatusIds.ToList();
            var toAdd2 = selectIds2.Except(existingIds2).ToList();
            var toRemove2 = existingIds2.Except(selectIds2).ToList();
            product.ProductProductStatuses = product.ProductProductStatuses.Where(x => x.ProductStatusID.HasValue && !toRemove2.Contains((x.ProductStatusID.Value)).Equals(toAdd2)).ToList();



            foreach (var item in toAdd)
            {
                product.ProductSizes.Add(new ProductSize()
                {
                    SizeID = item
                });

                product.ProductColors.Add(new ProductColor()
                {
                    ColorID = item
                });

                product.ProductProductStatuses.Add(new ProductProductStatus()
                {
                    ProductStatusID = item
                });

            }
            _context.Products.Update(product);
        }


        //public List<Product> GetAll(Expression<Func<Product, bool>> predicate = null)
        //{
        //    return predicate is null
        //    ?
        //        _context.Set<Product>().Include(x => x.Colors).Include(x => x.Sizes).Include(x => x.Category).Include(x => x.ProductStatuses).ToList()
        //    :
        //        _context.Set<Product>().Include(x => x.Colors).Include(x => x.Sizes).Include(x => x.Category).Include(x => x.ProductStatuses).Where(predicate).ToList(); ;
        //}

    }
}
