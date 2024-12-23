using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using ProjetoTeste.Context;
using ProjetoTeste.Infrastructure.Default;
using ProjetoTeste.Models;

namespace ProjetoTeste.Infrastructure.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
