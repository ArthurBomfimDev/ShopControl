using ProjetoTeste.Infrastructure.Brands;
using ProjetoTeste.Infrastructure.Clients;
using ProjetoTeste.Infrastructure.Default;
using ProjetoTeste.Infrastructure.Orders;
using ProjetoTeste.Infrastructure.Products;
using ProjetoTeste.Infrastructure.UnitOfWork;
using ProjetoTeste.Models;
using ProjetoTeste.Services;

namespace ProjetoTeste.Extensions
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
