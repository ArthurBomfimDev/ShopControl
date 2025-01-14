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
    public async Task<ActionResult<OutputProduct>> Get(long id)
    {
        var product = await _productService.Get(id);
        return Ok(product);
    }

    [HttpGet("Id/Multiple")]
    public async Task<ActionResult<OutputProduct>> GetListByListId(List<long> listId)
    {
        var product = await _productService.GetListByListId(listId);
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
    public async Task<ActionResult> Update(InputIdentityUpdateBrand inputIdentityUpdateProduct)
    {
        var updateProduct = await _productService.Update(inputIdentityUpdateProduct);
        if (!updateProduct.Success)
        {
            return BadRequest(updateProduct);
        }
        return Ok(updateProduct);
    }

    [HttpPut("Multiple")]
    public async Task<ActionResult> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateProduct)
    {
        var updateProduct = await _productService.UpdateMultiple(listInputIdentityUpdateProduct);
        if (!updateProduct.Success)
        {
            return BadRequest(updateProduct);
        }
        return Ok(updateProduct);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete( long id)
    {
        var deletePrdocut = await _productService.Delete(id);
        if (!deletePrdocut.Success)
        {
            return BadRequest(deletePrdocut.Message);
        }
        return Ok(deletePrdocut.Message);
    }

    [HttpDelete("Multiple")]
    public async Task<ActionResult> Delete(List<long> listId)
    {
        var deletePrdocut = await _productService.DeleteMultiple(listId);
        if (!deletePrdocut.Success)
        {
            return BadRequest(deletePrdocut.Message);
        }
        return Ok(deletePrdocut.Message);
    }
}