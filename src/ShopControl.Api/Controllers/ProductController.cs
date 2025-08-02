using Microsoft.AspNetCore.Mvc;
using ShopControl.Api.Controllers.Base;
using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Product;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service;
using ShopControl.Infrastructure.Interface.UnitOfWork;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Api.Controllers;

public class ProductController : BaseController<IProductService, ProductDTO, Product, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct>
{
    private readonly IProductService _productService;

    public ProductController(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork, productService)
    {
        _productService = productService;
    }

    [HttpPost("GetListByBrandId")]
    public async Task<ActionResult<OutputProduct>> GetListByBrandId(InputIdentityViewBrand inputIdentifyViewBrand)
    {
        var product = await _productService.GetListByBrandId(inputIdentifyViewBrand);
        return Ok(product);
    }

    [HttpPost("Send/Image")]
    public async Task<ActionResult<FormFile>> Image(IFormFile file)
    {
        return Ok(file);
    }
}