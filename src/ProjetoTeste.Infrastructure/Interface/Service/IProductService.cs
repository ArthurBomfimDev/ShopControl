using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IProductService
{
    Task<List<OutputProduct>> GetAll();
    Task<OutputProduct> Get(InputIdentifyViewProduct inputIdentifyViewProduct);
    Task<List<OutputProduct>> GetListByListId(List<InputIdentifyViewProduct> listInputIdentifyViewProduct);
    Task<List<OutputProduct>> GetListByBrandId(InputIdentifyViewBrand inputIdentifyViewBrand);
    Task<BaseResponse<OutputProduct>> Create(InputCreateProduct inputCreateProduct);
    Task<BaseResponse<List<OutputProduct>>> CreateMultiple(List<InputCreateProduct> listinputCreateProduct);
    Task<BaseResponse<bool>> Update(InputIdentityUpdateProduct inputIdentityUpdateProduct);
    Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct);
    Task<BaseResponse<bool>> Delete(InputIdentifyDeleteProduct inputIdentifyDeleteProduct);
    Task<BaseResponse<bool>> DeleteMultiple(List<InputIdentifyDeleteProduct> listInputIdentifyDeleteProduct);
}