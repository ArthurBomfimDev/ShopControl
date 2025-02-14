using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetAllWithProductOrders();
        Task<List<Order>> GetByIdWithProductOrders(long id);
        Task<List<Order>> GetListByListIdWhithProductOrders(List<long> listId);
        Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
        Task<OutputMaxSaleValueProduct?> BestSellerProduct();
        Task<OutputMaxSaleValueProduct?> LeastSoldProduct();
        Task<decimal> Total();
        Task<HighestAverageSalesValue?> HighestAverageSalesValue();
        Task<OutputBrandBestSeller?> BrandBestSeller();
        Task<OutputCustomerOrder?> BiggestBuyer();
    }
}