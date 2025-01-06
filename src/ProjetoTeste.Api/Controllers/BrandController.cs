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
        if (!brandList.Success)
        {
            return NotFound(brandList.Message);
        }
        return Ok(brandList.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OutputBrand?>> Get(int id)
    {
        var brand = await _brandService.Get(id);
        if (!brand.Success)
        {
            return NotFound(brand.Message);
        }
        return Ok(brand.Value);
    }

    [HttpPost]
    public async Task<ActionResult<OutputBrand>> Create(InputCreateBrand brand)
    {
        var createdBrand = await _brandService.Create(brand);
        if (!createdBrand.Success)
        {
            return BadRequest(createdBrand.Message);
        }
        return Ok(createdBrand.Value);
    }

    [HttpPut]
    public async Task<ActionResult> Update(long id, InputUpdateBrand input)
    {
        var updateBrand = await _brandService.Update(id, input);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand.Message);
        }
        return Ok(updateBrand.Message);
    }

    [HttpDelete]
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