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
    Task<Response<OutputSellProduct>> BestSellerProduct();
    Task<Response<OutputSellProduct>> LeastSoldProduct();
    Task<Response<List<OutputSellProduct>>> TopSellers();
    Task<Response<OutputCustomerOrder>> BiggestBuyer();
    Task<Response<OutputCustomerOrder>> BiggestBuyerPrice();
    Task<Response<OutputBrandBestSeller>> BrandBestSeller();
    Task<Response<decimal>> Avarege();
    Task<Response<decimal>> Total();
}