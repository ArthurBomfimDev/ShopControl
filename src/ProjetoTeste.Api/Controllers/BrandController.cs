using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Brands;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers;

public class BrandController : BaseController
{
    private readonly IBrandService _brandService;

    public BrandController(IUnitOfWork unitOfWork, IBrandService brandService) : base(unitOfWork)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OutputBrand?>>> GetAll()
    {
        var brandList = await _brandService.GetAll();
        return Ok(brandList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OutputBrand?>> Get(int id)
    {
        var brand = await _brandService.Get(id);
        return Ok(brand);
    }

    [HttpGet("Products")]
    public async Task<ActionResult<List<OutputBrand?>>> GetAllAndProducts()
    {
        var brandList = await _brandService.GetAllAndProduct();
        return Ok(brandList);
    }

    [HttpGet("Products{id}")]
    public async Task<ActionResult<List<OutputBrand?>>> GetAndProducts(long id)
    {
        var brandList = await _brandService.GetAndProduct(id);
        return Ok(brandList);
    }

    [HttpPost]
    public async Task<ActionResult<OutputBrand>> Create(InputCreateBrand brand)
    {
        var createdBrand = await _brandService.Create(brand);
        if (!createdBrand.Success)
        {
            return BadRequest(createdBrand.Message);
        }
        return Ok(createdBrand.Content);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(long id, InputUpdateBrand input)
    {
        var updateBrand = await _brandService.Update(id, input);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand.Message);
        }
        return Ok(updateBrand.Message);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        var deleteBrand = await _brandService.Delete(id);
        if (!deleteBrand.Success)
        {
            return BadRequest(deleteBrand.Message);
        }
        return Ok(deleteBrand.Message);
    }
}