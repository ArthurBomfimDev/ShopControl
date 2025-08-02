using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository.Base;

namespace ProjetoTeste.Domain.Interface.Repository;
public interface IProductOrderRepository : IBaseRepository<ProductOrderDTO>
{
    List<ProductOrderDTO> GetListByListOrderId(List<long> listOrderId);
}