using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IOrderService
{
    Task<BaseResponse<List<OutputOrder>>> GetAll();
    Task<BaseResponse<List<OutputOrder>>> Get(long id);
    Task<BaseResponse<OutputOrder>> Delete(List<long> ids);
    Task<BaseResponse<OutputOrder>> Create(InputCreateOrder input);
    Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder input);
    Task<OutputMaxSaleValueProduct> BestSellerProduct();
    Task<OutputMaxSaleValueProduct> LeastSoldProduct();
    Task<List<OutputMaxSaleValueProduct>> TopSellers();
    Task<OutputCustomerOrder> BiggestBuyer();
    Task<OutputCustomerOrder> BiggestBuyerPrice();
    Task<OutputBrandBestSeller> BrandBestSeller();
    Task<decimal> Avarege();
    Task<decimal> Total();
}