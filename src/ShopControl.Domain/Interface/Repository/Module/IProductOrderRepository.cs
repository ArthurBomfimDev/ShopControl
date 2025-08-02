using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository.Base;

namespace ShopControl.Domain.Interface.Repository;
public interface IProductOrderRepository : IBaseRepository<ProductOrderDTO>
{
    List<ProductOrderDTO> GetListByListOrderId(List<long> listOrderId);
}