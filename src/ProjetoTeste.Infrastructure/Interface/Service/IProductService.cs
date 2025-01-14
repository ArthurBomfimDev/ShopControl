using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IProductService
{
    Task<List<OutputProduct>> GetAll();
    Task<OutputProduct> Get(long id);
    Task<List<OutputProduct>> GetListByListId(List<long> idList);
    Task<List<OutputProduct>> GetListByBrandId(long id);
    Task<BaseResponse<List<OutputProduct>>> Create(InputCreateProduct inputCreateProduct);
    Task<BaseResponse<List<OutputProduct>>> CreateMultiple(List<InputCreateProduct> inputCreateProduct);
    Task<BaseResponse<bool>> Update(InputIdentityUpdateBrand inputIdentityUpdateProduct);
    Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> inputIdentityUpdateProduct);
    Task<BaseResponse<bool>> Delete(long id);
    Task<BaseResponse<bool>> DeleteMultiple(List<long> idList);
}