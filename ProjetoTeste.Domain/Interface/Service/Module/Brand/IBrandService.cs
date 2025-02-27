using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Domain.Interface.Service
{
    public interface IBrandService : IBaseService<BrandDTO, InputCreateBrand, InputIdentityUpdateBrand, InputIdentifyDeleteBrand, InputIdentityViewBrand, OutputBrand> { }
}