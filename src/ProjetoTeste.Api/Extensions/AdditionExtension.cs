using ProjetoEstagioAPI.Infrastructure.Brands;
using ProjetoEstagioAPI.Infrastructure.Clients;
using ProjetoEstagioAPI.Infrastructure.Default;
using ProjetoEstagioAPI.Infrastructure.Orders;
using ProjetoEstagioAPI.Infrastructure.Products;
using ProjetoEstagioAPI.Infrastructure.UnitOfWork;
using ProjetoEstagioAPI.Models;
using ProjetoEstagioAPI.Services;

namespace ProjetoEstagioAPI.Extensions
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

            return services;
        }
    }
}
