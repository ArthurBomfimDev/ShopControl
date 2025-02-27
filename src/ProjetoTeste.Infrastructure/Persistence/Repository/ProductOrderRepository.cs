using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class ProductOrderRepository : BaseRepository<ProductOrder, ProductOrderDTO>, IProductOrderRepository
{
    public ProductOrderRepository(AppDbContext context) : base(context)
    {
    }

    public List<ProductOrderDTO> GetListByListOrderId(List<long> listOrderId)
    {
        var getListByListOrderId = _dbSet.Where(i => listOrderId.Contains(i.OrderId)).ToList();
        return getListByListOrderId.Select(i => (ProductOrderDTO)i).ToList();
    }
}