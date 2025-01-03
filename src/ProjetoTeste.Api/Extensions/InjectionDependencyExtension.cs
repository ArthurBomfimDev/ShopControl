using ProjetoTeste.Infrastructure.Application.Service;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Repository;
namespace ProjetoTeste.Api.Extensions;

public static class InjectionDependencyExtension
{
    public static IServiceCollection ConfigureInjectionDependency(this IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}