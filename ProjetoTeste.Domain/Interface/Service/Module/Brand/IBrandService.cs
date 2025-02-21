using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Interface.Service.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService : IBaseService<BrandDTO, InputCreateBrand, InputIdentityUpdateBrand, InputIdentifyDeleteBrand, InputIdentityViewBrand, OutputBrand> { }
}