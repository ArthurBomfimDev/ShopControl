using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OutputProduct>>> GetAll()
    {
        var productList = await _productService.GetAll();
        return Ok(productList);
    }

    [HttpGet("Id")]
    public async Task<ActionResult<OutputProduct>> Get(InputIdentifyViewProduct inputIdentifyViewProduc)
    {
        var product = await _productService.Get(inputIdentifyViewProduc);
        return Ok(product);
    }

    [HttpPost("Id/Multiple")]
    public async Task<ActionResult<OutputProduct>> GetListByListId([FromBody] List<InputIdentifyViewProduct> listInputIdentifyViewProduct)
    {
        var product = await _productService.GetListByListId(listInputIdentifyViewProduct);
        return Ok(product);
    }

    [HttpGet("GetListByBrandId")]
    public async Task<ActionResult<OutputProduct>> GetListByBrandId([FromQuery] InputIdentifyViewBrand inputIdentifyViewBrand)
    {
        var product = await _productService.GetListByBrandId(inputIdentifyViewBrand);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<List<OutputProduct>>>> Create(InputCreateProduct inputCreateProduct)
    {
        var createProduct = await _productService.Create(inputCreateProduct);
        if (createProduct.Success == false)
        {
            return BadRequest(createProduct);
        }
        return Ok(createProduct);
    }

    [HttpPost("Multiple")]
    public async Task<ActionResult<BaseResponse<List<OutputProduct>>>> Create(List<InputCreateProduct> listInputCreateProduct)
    {
        var createProduct = await _productService.CreateMultiple(listInputCreateProduct);
        if (createProduct.Success == false)
        {
            return BadRequest(createProduct);
        }
        return Ok(createProduct);
    }

    [HttpPut]
    public async Task<ActionResult> Update(InputIdentityUpdateProduct inputIdentityUpdateProduct)
    {
        var updateProduct = await _productService.Update(inputIdentityUpdateProduct);
        if (!updateProduct.Success)
        {
            return BadRequest(updateProduct);
        }
        return Ok(updateProduct);
    }

    [HttpPut("Multiple")]
    public async Task<ActionResult> Update(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
    {
        var updateProduct = await _productService.UpdateMultiple(listInputIdentityUpdateProduct);
        if (!updateProduct.Success)
        {
            return BadRequest(updateProduct);
        }
        return Ok(updateProduct);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(InputIdentifyDeleteProduct inputIdentifyDeleteProduct)
    {
        var deletePrdocut = await _productService.Delete(inputIdentifyDeleteProduct);
        if (!deletePrdocut.Success)
        {
            return BadRequest(deletePrdocut.Message);
        }
        return Ok(deletePrdocut.Message);
    }

    [HttpDelete("Multiple")]
    public async Task<ActionResult> Delete(List<InputIdentifyDeleteProduct> listInputIdentifyDeleteProduct)
    {
        var deletePrdocut = await _productService.DeleteMultiple(listInputIdentifyDeleteProduct);
        if (!deletePrdocut.Success)
        {
            return BadRequest(deletePrdocut.Message);
        }
        return Ok(deletePrdocut.Message);
    }
}