using AutoMapper;
using IS_Task.Areas.Admin.Models.RequestModels;
using IS_Task.Areas.Admin.Models.ResponseModels;
using IS_Task.Data.Entities;
using IS_Task.Models.ResponseModels;

namespace IS_Task.Mappings
{
    public class BusinessMapperProfile : Profile
    {
        public BusinessMapperProfile()
        {
            CreateMap<CreateCategoryModel, Category>();

            CreateMap<GetCategoryByIdResponseModel, EditCategoryModel>();

            CreateMap<EditCategoryModel, Category>();

            CreateMap<CreateProductModel, Product>();

            CreateMap<EditProductModel, Product>();

            CreateMap<GetProductByIdResponseModel, EditProductModel>();

            CreateMap<CartItem, CartItemResponseModel>()
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.LineTotal, opt => opt.MapFrom(src => src.Product.Price * src.Quantity));

            CreateMap<Cart, GetCartResponseModel>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CartItems.Sum(x => x.Product.Price * x.Quantity)));
        }
    }
}
