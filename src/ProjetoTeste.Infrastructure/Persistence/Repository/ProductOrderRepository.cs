using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class ProductOrderRepository : Repository<ProductOrder>, IProductOrderRepository
{
    public ProductOrderRepository(AppDbContext context) : base(context)
    {
    }
}