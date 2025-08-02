using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository.Base;

namespace ProjetoTeste.Domain.Interface.Repository;

public interface IOrderRepository : IBaseRepository<OrderDTO>
{
    Task<List<OrderDTO>> GetAllWithProductOrders();
    Task<List<OrderDTO>> GetByIdWithProductOrders(long id);
    Task<List<OrderDTO>> GetListByListIdWhithProductOrders(List<long> listId);
    Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
    Task<OutputMaxSaleValueProduct?> BestSellerProduct();
    Task<OutputMaxSaleValueProduct?> LeastSoldProduct();
    Task<decimal> Total();
    Task<HighestAverageSalesValue?> HighestAverageSalesValue();
    Task<OutputBrandBestSeller?> BrandBestSeller();
    Task<OutputCustomerOrder?> BiggestBuyer();
}
