using ProjetoTeste.Api.Controllers.Base;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Api.Controllers;
//Maestro
public class BrandController(IUnitOfWork unitOfWork, IBrandService brandService) : BaseController<IBrandService, BrandDTO, Brand, InputCreateBrand, InputUpdateBrand ,InputIdentityUpdateBrand, InputIdentifyDeleteBrand, InputIdentityViewBrand, OutputBrand>(unitOfWork, brandService)
{
}