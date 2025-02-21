using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Interface.Service.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IOrderService : IBaseService<OrderDTO, InputCreateOrder, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, InputIdentifyViewOrder, OutputOrder>
{
    Task<List<OutputOrder>> GetByIdWithProducts(InputIdentifyViewOrder inputIdentifyViewOrder);
    Task<OutputMaxSaleValueProduct> BestSellerProduct();
    Task<OutputMaxSaleValueProduct> LeastSoldProduct();
    Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
    Task<OutputCustomerOrder> BiggestBuyer();
    Task<OutputBrandBestSeller> BrandBestSeller();
    Task<HighestAverageSalesValue> HighestAverageSalesValue();
    Task<string> Total();
    Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder inputCreateProductOrder);
    Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrderMultiple(List<InputCreateProductOrder> listinputCreateProductOrder);
}