using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Order;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository.Base;

namespace ShopControl.Domain.Interface.Repository;

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
