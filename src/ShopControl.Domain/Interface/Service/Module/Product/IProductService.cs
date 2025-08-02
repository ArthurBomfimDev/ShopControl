using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Product;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service.Base;


namespace ShopControl.Domain.Interface.Service;

public interface IProductService : IBaseService<ProductDTO, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct>
{
    Task<List<OutputProduct>> GetListByBrandId(InputIdentityViewBrand inputIdentifyViewBrand);
}