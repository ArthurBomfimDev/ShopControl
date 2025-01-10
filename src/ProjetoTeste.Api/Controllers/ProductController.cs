using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<OutputProduct>> GetListByListId(List<long> idList)
    {
        var product = await _productService.GetListByListId(idList);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<List<OutputProduct>>>> Create(List<InputCreateProduct> input)
    {
        var createProduct = await _productService.Create(input);
        if (createProduct.Success == false)
        {
            return BadRequest(createProduct);
        }
        return Ok(createProduct);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromQuery] List<long> idList, [FromBody] List<InputUpdateProduct> input)
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