using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IOrderService
{
    Task<BaseResponse<List<OutputOrder>>> GetAll();
    Task<BaseResponse<List<OutputOrder>>> Get(long id);
    Task<List<OutputOrder>> GetListByListId(List<long> idList);
    Task<BaseResponse<List<OutputOrder>>> Create(InputCreateOrder inputCreateOrder);
    Task<BaseResponse<List<OutputOrder>>> CreateMultiple(List<InputCreateOrder> inputCreateOrder);
    Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder inputCreateProductOrder);
    Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrder(List<InputCreateProductOrder> inputCreateProductOrder);
    Task<OutputMaxSaleValueProduct> BestSellerProduct();
    Task<OutputMaxSaleValueProduct> LeastSoldProduct();
    Task<List<OutputMaxSaleValueProduct>> TopSellers();
    Task<OutputCustomerOrder> BiggestBuyer();
    Task<OutputCustomerOrder> BiggestBuyerPrice();
    Task<OutputMaxSaleValueProduct> OrderBestSeller();
    Task<decimal> Avarege();
    Task<decimal> Total();
}