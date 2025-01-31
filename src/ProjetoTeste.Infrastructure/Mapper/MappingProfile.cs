using AutoMapper;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Mapper;

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
            .ForMember(i => i.listProductOrders, opt => opt.MapFrom(src => src.ListProductOrder));
        CreateMap<Order, OrderDTO>().ReverseMap();

        CreateMap<ProductOrder, OutputProductOrder>();
        CreateMap<ProductOrder, ProductOrderDTO>().ReverseMap();

        CreateMap(typeof(BaseResponse<>), typeof(BaseResponse<>));
    }
}