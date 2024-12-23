using Microsoft.AspNetCore.Mvc;

using ProjeotTeste.Arguments.Products;
using ProjeotTeste.Models;
using ProjeotTeste.Services;

namespace ProjetoTeste.Api.Controllers;
[Route("Produtos")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("Exibir todos os Produtos")]
    public async Task<ActionResult<List<OutputProduct>>> GetAll()
    {
        var productList = await _productService.GetAll();
        if (productList.Success == false)
        {
            return BadRequest(productList.Message);
        }
        return Ok(productList.Entity);
    }
    [HttpGet("Buscar produto por Id")]
    public async Task<ActionResult<OutputProduct>> Get(long id)
    {
        var product = await _productService.Get(id);
        if (product.Success == false)
        {
            return BadRequest(product.Message);
        }
        return Ok(product.Entity);
    }
    [HttpDelete("Deletar Produto")]
    public async Task<ActionResult> Delete(long id)
    {
        var deletePrdocut = await _productService.Delete(id);
        if (!deletePrdocut.Success)
        {
            return BadRequest(deletePrdocut.Message);
        }
        return Ok(deletePrdocut.Message);
    }
    [HttpPost("Criar Produto")]
    public async Task<ActionResult<OutputProduct>> Create(InputCreateProduct input)
    {
        var createProduct = await _productService.Create(input);
        if (createProduct.Success == false)
        {
            return BadRequest(createProduct.Message);
        }
        return Ok(createProduct.Entity);
    }
    [HttpPut("Atualizar Produto")]
    public async Task<ActionResult> Update(long id, InputUpdateProduct input)
    {
        var updateProduct = await _productService.Update(id, input);
        if (!updateProduct.Success)
        {
            return BadRequest(updateProduct.Message);
        }
        return Ok(updateProduct.Message);
    }
}