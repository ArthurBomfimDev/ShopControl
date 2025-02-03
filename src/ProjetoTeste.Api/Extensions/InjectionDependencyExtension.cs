using ProjetoTeste.Infrastructure.Application;
using ProjetoTeste.Infrastructure.Application.Service.Order;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Interface.ValidateService;
using ProjetoTeste.Infrastructure.Mapper;
using ProjetoTeste.Infrastructure.Persistence;
using ProjetoTeste.Infrastructure.Persistence.Repository;

namespace ProjetoTeste.Api.Extensions;

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
        services.AddScoped<IOrderValidateService, OrderValidateService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}