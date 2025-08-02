using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Brand;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service.Base;

namespace ShopControl.Domain.Interface.Service
{
    public interface IBrandService : IBaseService<BrandDTO, InputCreateBrand, InputUpdateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, InputIdentityViewBrand, OutputBrand> { }
}