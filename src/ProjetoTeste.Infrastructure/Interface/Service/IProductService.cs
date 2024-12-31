using ProjetoTeste.Arguments.Arguments.Products;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IProductService
{
    Task<Response<List<OutputProduct>>> GetAll();
    Task<Response<OutputProduct>> Get(long id);
    Task<Response<bool>> Delete(long id);
    Task<Response<OutputProduct>> Create(InputCreateProduct product);
    Task<Response<bool>> Update(long id, InputUpdateProduct product);
}
