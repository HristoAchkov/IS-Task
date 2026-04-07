using AutoMapper;
using IS_Task.Areas.Admin.Models.RequestModels;
using IS_Task.Core.Interfaces;
using IS_Task.Data;
using IS_Task.Data.Entities;
using IS_Task.Models.RequestModels;
using IS_Task.Models.ResponseModels;
using IS_Task.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Task.Core.Services
{
    public class ProductService(
        ApplicationDbContext dbContext,
        IMapper mapper) : IProductService
    {
        public async Task CreateProduct(CreateProductModel productModel)
        {
            var product = mapper.Map<Product>(productModel);

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<GetAllProductsResponseModel>> GetAllProducts()
        {
            return await dbContext.Products
                .Select(x => new GetAllProductsResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Status = x.Status,
                    CategoryId = x.CategoryId,
                })
                .ToListAsync();
        }

        public async Task<GetProductByIdResponseModel> GetProductById(long id)
        {
            return await dbContext.Products
                .Select(x => new GetProductByIdResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Status = x.Status,
                    CategoryId = x.CategoryId,
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateProduct(EditProductModel editProductModel)
        {
            var existingProduct = await dbContext.Products
                .FindAsync(editProductModel.Id);

            if (existingProduct is not null)
            {
                mapper.Map(editProductModel, existingProduct);
                existingProduct.UpdatedAt = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(long id)
        {
            await dbContext.Products
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task SoftDeleteProduct(long id)
        {
            var product = await dbContext.Products
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (product is not null)
            {
                product.Status = Shared.Enums.ProductStatus.Archived;

                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<GetAllProductsServiceModel> AllAsync(
            int? categoryId,
            int currentPage,
            int productsPerPage)
        {
            var query = dbContext.Products
                .AsNoTracking()
                .Where(p => p.Status == ProductStatus.Active)
                .Where(p => p.Category.Status == CategoryStatus.Active)
                .AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            var total = await query.CountAsync();

            var products = await query
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new GetAllProductsResponseModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Status = p.Status
                })
                .ToListAsync();

            return new GetAllProductsServiceModel
            {
                TotalProductsCount = total,
                TotalPages = (int)Math.Ceiling((double)total / productsPerPage),
                Products = products
            };
        }
    }
}
