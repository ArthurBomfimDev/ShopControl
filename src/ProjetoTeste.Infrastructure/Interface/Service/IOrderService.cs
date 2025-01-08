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
    Task<BaseResponse<OutputSellProduct>> BestSellerProduct();
    Task<BaseResponse<OutputSellProduct>> LeastSoldProduct();
    Task<BaseResponse<List<OutputSellProduct>>> TopSellers();
    Task<BaseResponse<OutputCustomerOrder>> BiggestBuyer();
    Task<BaseResponse<OutputCustomerOrder>> BiggestBuyerPrice();
    Task<BaseResponse<OutputBrandBestSeller>> BrandBestSeller();
    Task<BaseResponse<decimal>> Avarege();
    Task<BaseResponse<decimal>> Total();
}