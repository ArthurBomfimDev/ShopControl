using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjeotTeste.ProjetoTeste

using ProjeotTeste.Infrastructure.Brands;
using ProjeotTeste.Infrastructure.UnitOfWork;
using ProjeotTeste.Mapping.Brands;
using ProjeotTeste.Services;

namespace ProjetoTeste.Api.Controllers;

[Route("marca")]
[ApiController]
public class BrandController : Controller
{
    private readonly BrandService _brandService;
    private readonly IUnitOfWork _unitOfWork;

    public BrandController(IUnitOfWork unitOfWork, BrandService brandService)
    {
        _brandService = brandService;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("ExibirTodasasMarcas")]
    public async Task<ActionResult<List<OutputBrand?>>> GetAll()
    {
        var brandList = await _brandService.GetAll();
        if (!brandList.Success)
        {
            return NotFound(brandList.Message);
        }
        return Ok(brandList.Entity);
    }
    [HttpGet("ProcurarMarca")]
    public async Task<ActionResult<OutputBrand?>> Get(int id)
    {
        var brand = await _brandService.Get(id);
        if (!brand.Success)
        {
            return NotFound(brand.Message);
        }
        return Ok(brand.Entity);
    }
    [HttpPost("Criar Marca")]
    public async Task<ActionResult<OutputBrand>> Create(InputCreateBrand brand)
    {
        var createdBrand = await _brandService.Create(brand);
        if (!createdBrand.Success)
        {
            BadRequest(createdBrand.Message);
        }
        return Ok(createdBrand.Entity);
    }
    [HttpPut("AtualizarMarca")]
    public async Task<ActionResult> Update(long id, InputUpdateBrand input)
    {
        var updateBrand = await _brandService.Update(id, input);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand.Message);
        }
        return Ok(updateBrand.Message);
    }
    [HttpDelete("DeletarMarca")]
    public async Task<ActionResult> Delete(long id)
    {
        var deleteBrand = await _brandService.Delete(id);
        if (!deleteBrand.Success)
        {
            return BadRequest(deleteBrand.Message);
        }
        return Ok(deleteBrand.Message);
    }
    public override async void OnActionExecuted(ActionExecutedContext context)
    {
        await _unitOfWork.Commit();
        base.OnActionExecuted(context);
    }
}