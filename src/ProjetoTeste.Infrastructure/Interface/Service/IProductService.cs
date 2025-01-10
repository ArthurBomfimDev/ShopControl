using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IProductService
{
    Task<BaseResponse<List<OutputProduct>>> GetAll();
    Task<BaseResponse<List<OutputProduct>>> Get(List<long> idList);
    Task<BaseResponse<bool>> Delete(List<long> idList);
    Task<BaseResponse<List<OutputProduct>>> Create(List<InputCreateProduct> product);
    Task<BaseResponse<bool>> Update(List<long> idList, List<InputUpdateProduct> product);
}