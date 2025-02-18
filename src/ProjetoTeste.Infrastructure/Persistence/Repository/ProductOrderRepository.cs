using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class ProductOrderRepository : BaseRepository<ProductOrder, ProductOrderDTO>, IProductOrderRepository
{
    public ProductOrderRepository(AppDbContext context) : base(context)
    {
    }
}