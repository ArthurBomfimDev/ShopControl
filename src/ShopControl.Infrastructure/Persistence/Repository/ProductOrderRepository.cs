using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository;
using ShopControl.Infrastructure.Persistence.Context;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Infrastructure.Persistence.Repository;

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