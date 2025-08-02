using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.Crud;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Domain.Interface.Service;

public interface IOrderService : IBaseService<OrderDTO, InputCreateOrder, BaseInputUpdate_0, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, InputIdentifyViewOrder, OutputOrder>
{
    Task<List<OutputOrder>> GetByIdWithProducts(InputIdentifyViewOrder inputIdentifyViewOrder);
    Task<OutputMaxSaleValueProduct> BestSellerProduct();
    Task<OutputMaxSaleValueProduct> LeastSoldProduct();
    Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
    Task<OutputCustomerOrder> BiggestBuyer();
    Task<OutputBrandBestSeller> BrandBestSeller();
    Task<HighestAverageSalesValue> HighestAverageSalesValue();
    Task<string> Total();
}