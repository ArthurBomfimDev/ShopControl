using AutoMapper;
using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Brand;
using ShopControl.Arguments.Arguments.Customer;
using ShopControl.Arguments.Arguments.Order;
using ShopControl.Arguments.Arguments.Product;
using ShopControl.Arguments.Arguments.ProductOrder;
using ShopControl.Domain.DTO;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Brand, OutputBrand>();
        CreateMap<Brand, BrandDTO>().ReverseMap();

        CreateMap<Product, OutputProduct>();
        CreateMap<Product, ProductDTO>().ReverseMap();

        CreateMap<Customer, OutputCustomer>();
        CreateMap<Customer, CustomerDTO>().ReverseMap();

        CreateMap<Order, OutputOrder>()
            .ForMember(i => i.ListProductOrders, opt => opt.MapFrom(src => src.ListProductOrder));
        CreateMap<Order, OrderDTO>().ReverseMap();

        CreateMap<ProductOrder, OutputProductOrder>();
        CreateMap<ProductOrder, ProductOrderDTO>().ReverseMap();

        CreateMap(typeof(BaseResponse<>), typeof(BaseResponse<>));
    }
}