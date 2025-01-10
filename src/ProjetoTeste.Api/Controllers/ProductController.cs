using Microsoft.AspNetCore.Mvc;
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
        if (productList.Success == false)
        {
            return BadRequest(productList.Message);
        }
        return Ok(productList.Content);
    }

    [HttpGet("Id")]
    public async Task<ActionResult<OutputProduct>> Get(List<long> idList)
    {
        var product = await _productService.Get(idList);
        if (product.Success == false)
        {
            return BadRequest(product.Message);
        }
        return Ok(product.Content);
    }

    [HttpPost]
    public async Task<ActionResult<OutputProduct>> Create(List<InputCreateProduct> input)
    {
        var createProduct = await _productService.Create(input);
        if (createProduct.Success == false)
        {
            return BadRequest(createProduct.Message);
        }
        return Ok(createProduct.Content);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(List<long> idList, List<InputUpdateProduct> input)
    {
        var updateProduct = await _productService.Update(idList, input);
        if (!updateProduct.Success)
        {
            return BadRequest(updateProduct.Message);
        }
        return Ok(updateProduct.Message);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] List<long> idList)
    {
        var deletePrdocut = await _productService.Delete(idList);
        if (!deletePrdocut.Success)
        {
            return BadRequest(deletePrdocut.Message);
        }
        return Ok(deletePrdocut.Message);
    }
}