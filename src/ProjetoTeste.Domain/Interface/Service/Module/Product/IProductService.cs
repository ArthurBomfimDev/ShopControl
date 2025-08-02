using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Service.Base;


namespace ProjetoTeste.Domain.Interface.Service;

public interface IProductService : IBaseService<ProductDTO, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct>
{
    Task<List<OutputProduct>> GetListByBrandId(InputIdentityViewBrand inputIdentifyViewBrand);
}