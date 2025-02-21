//using Microsoft.AspNetCore.Mvc;
//using ProjetoTeste.Arguments.Arguments;
//using ProjetoTeste.Arguments.Arguments.Product;
//using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
//using ProjetoTeste.Infrastructure.Persistence.Entity;

//namespace ProjetoTeste.Api.Controllers;

//public class ProductController : BaseController<IProductService, Product, InputCreateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct>
//{
//    private readonly IProductService _productService;

//    public ProductController(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork, productService)
//    {
//        _productService = productService;
//    }

//    [HttpPost("GetListByBrandId")]
//    public async Task<ActionResult<OutputProduct>> GetListByBrandId(InputIdentityViewBrand inputIdentifyViewBrand)
//    {
//        var product = await _productService.GetListByBrandId(inputIdentifyViewBrand);
//        return Ok(product);
//    }
//}