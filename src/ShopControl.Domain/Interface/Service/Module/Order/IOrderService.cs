using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Base.Crud;
using ShopControl.Arguments.Arguments.Order;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service.Base;

namespace ShopControl.Domain.Interface.Service;

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