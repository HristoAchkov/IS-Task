using AutoMapper;
using IS_Task.Data.Entities;
using IS_Task.Models.RequestModels;
using IS_Task.Models.ResponseModels;

namespace IS_Task.Mappings
{
    public class BusinessMapperProfile : Profile
    {
        public BusinessMapperProfile()
        {
            CreateMap<GetCategoryByIdResponseModel, EditCategoryModel>();

            CreateMap<EditCategoryModel, Category>();

            CreateMap<CreateProductModel, Product>();
        }
    }
}
