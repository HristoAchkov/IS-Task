using AutoMapper;
using IS_Task.Core.Interfaces;
using IS_Task.Data;
using IS_Task.Data.Entities;
using IS_Task.Models.RequestModels;
using IS_Task.Models.ResponseModels;
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
                }).ToListAsync();
        }
    }
}
