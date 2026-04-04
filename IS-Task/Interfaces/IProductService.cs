using IS_Task.Models.RequestModels;
using IS_Task.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Task.Core.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductModel productModel);
        Task<List<GetAllProductsResponseModel>> GetAllProducts();
    }
}
