using ProjetoTeste.Infrastructure.Application.Service;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Repository;

namespace ProjetoTeste.Api.Extensions
{
    public static class AdditionExtension
    {
        public static IServiceCollection ConfigureAddition(this IServiceCollection services)
        {
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<BrandService>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();

            return services;
        }
    }
}
