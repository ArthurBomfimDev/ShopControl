using ShopControl.Api.Controllers.Base;
using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Brand;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service;
using ShopControl.Infrastructure.Interface.UnitOfWork;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Api.Controllers;
//Maestro
public class BrandController(IUnitOfWork unitOfWork, IBrandService brandService) : BaseController<IBrandService, BrandDTO, Brand, InputCreateBrand, InputUpdateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, InputIdentityViewBrand, OutputBrand>(unitOfWork, brandService)
{
}