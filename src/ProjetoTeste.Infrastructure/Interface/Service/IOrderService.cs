using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IOrderService
{
    Task<Response<List<OutputOrder>>> GetAll();
    Task<Response<List<Order>>> Get(long id);
    Task<Response<OutputOrder>> Delete(long id);
    Task<Response<OutputOrder>> Create(InputCreateOrder input);
    Task<Response<Order>> Total();
    Task<Response<OutputProductOrder>> Add(InputCreateProductOrder input);
}
