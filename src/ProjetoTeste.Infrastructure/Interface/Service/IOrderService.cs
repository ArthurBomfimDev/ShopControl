using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IOrderService
{
    Task<BaseResponse<List<OutputOrder>>> GetAll();
    Task<BaseResponse<List<OutputOrder>>> Get(InputIdentifyViewOrder inputIdentifyViewOrder);
    Task<BaseResponse<List<OutputOrder>>> GetListByListId(List<InputIdentifyViewOrder> listInputIdentifyViewOrder);
    Task<OutputMaxSaleValueProduct> BestSellerProduct();
    Task<OutputMaxSaleValueProduct> LeastSoldProduct();
    Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
    Task<OutputCustomerOrder> BiggestBuyer();
    Task<OutputBrandBestSeller> BrandBestSeller();
    Task<HighestAverageSalesValue> HighestAverageSalesValue();
    Task<string> Total();
    Task<BaseResponse<OutputOrder>> Create(InputCreateOrder inputCreateOrder);
    Task<BaseResponse<List<OutputOrder>>> CreateMultiple(List<InputCreateOrder> listinputCreateOrder);
    Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder inputCreateProductOrder);
    Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrderMultiple(List<InputCreateProductOrder> listinputCreateProductOrder);
}