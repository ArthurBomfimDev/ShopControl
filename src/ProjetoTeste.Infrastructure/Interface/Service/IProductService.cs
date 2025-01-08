using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IProductService
{
    Task<BaseResponse<List<OutputProduct>>> GetAll();
    Task<BaseResponse<OutputProduct>> Get(long id);
    Task<BaseResponse<bool>> Delete(long id);
    Task<BaseResponse<List<OutputProduct>>> Create(List<InputCreateProduct> product);
    Task<BaseResponse<bool>> Update(long id, List<InputUpdateProduct> product);
}
