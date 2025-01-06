using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IOrderService
{
    Task<Response<List<OutputOrder>>> GetAll();
    Task<Response<List<OutputOrder>>> Get(long id);
    Task<Response<OutputOrder>> Delete(long id);
    Task<Response<OutputOrder>> Create(InputCreateOrder input);
    Task<Response<OutputProductOrder>> Add(InputCreateProductOrder input);
    Task<OutputSellProduct> BestSellerProduct();
    Task<OutputSellProduct> LesatSoldProduct();
    Task<List<OutputSellProduct>> TopSellers();
    Task<OutputCustomerOrder> BiggestBuyer();
    Task<OutputCustomerOrder> BiggestBuyerPrice();
    Task<OutputBrandBestSeller> BrandBestSeller();
    Task<decimal> Avarege();
    Task<decimal> Total();
}