using ShopControl.Domain.Interface.Repository;
using ShopControl.Domain.Interface.Service;
using ShopControl.Domain.Interface.Service.Module.ProductOrder;
using ShopControl.Domain.Service;
using ShopControl.Domain.Service.Module.ProductOrder;
using ShopControl.Infrastructure.Application;
using ShopControl.Infrastructure.Application.Service.Order;
using ShopControl.Infrastructure.Interface.UnitOfWork;
using ShopControl.Infrastructure.Interface.ValidateService;
using ShopControl.Infrastructure.Mapper;
using ShopControl.Infrastructure.Persistence;
using ShopControl.Infrastructure.Persistence.Repository;

namespace ShopControl.Api.Extensions;

public static class InjectionDependencyExtension
{
    public static IServiceCollection ConfigureInjectionDependency(this IServiceCollection services) //Injeta as depdencias nos controllers
    {
        services.AddScoped<IBrandRepository, BrandRepository>(); //Quando precisar de um IBrandService ele criara uma nova instancia de BrandService para requesição HTTP, quando finalizar a requisição será descartada (uma por requisição)
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBrandValidateService, BrandValidateService>();
        services.AddScoped<ICustomerValidateService, CustomerValidateService>();
        services.AddScoped<IProductValidateService, ProductValidateService>();
        services.AddScoped<IProductOrderValidateService, ProductOrderValidateService>();
        services.AddScoped<IOrderValidateService, OrderValidateService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductOrderService, ProductOrderService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}