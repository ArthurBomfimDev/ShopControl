using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Api.Controllers.Base;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Api.Controllers;

public class ProductController : BaseController<IProductService, ProductDTO, Product, InputCreateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct>
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