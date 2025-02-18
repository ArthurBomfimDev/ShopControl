using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.Service.Base;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IProductService : IBaseService<Product, InputCreateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct>
{
    Task<List<OutputProduct>> GetListByBrandId(InputIdentityViewBrand inputIdentifyViewBrand);
}