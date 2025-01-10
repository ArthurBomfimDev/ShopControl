using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
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

    [HttpGet("Id")]
    public async Task<ActionResult<OutputBrand?>> Get(long id)
    {
        var brand = await _brandService.Get(id);
        return Ok(brand);
    }
    [HttpGet("Id/Multiple")]
    public async Task<ActionResult<OutputBrand?>> GetListByListId(List<long> idList)
    {
        var brand = await _brandService.GetListByListId(idList);
        return Ok(brand);
    }

    //[HttpGet("Products")]
    //public async Task<ActionResult<List<OutputBrand?>>> GetAllAndProducts()
    //{
    //    var brandList = await _brandService.GetAllAndProduct();
    //    return Ok(brandList);
    //}

    //[HttpGet("Products{id}")]
    //public async Task<ActionResult<List<OutputBrand?>>> GetAndProducts(long id)
    //{
    //    var brandList = await _brandService.GetAndProduct(id);
    //    return Ok(brandList);
    //}

    [HttpPost]
    public async Task<ActionResult<BaseResponse<List<OutputBrand>>>> Create(List<InputCreateBrand> brand)
    {
        var createdBrand = await _brandService.Create(brand);
        if (!createdBrand.Success)
        {
            return BadRequest(createdBrand.Message);
        }
        return Ok(createdBrand);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromQuery] List<long> ids, [FromBody] List<InputUpdateBrand> input)
    {
        var updateBrand = await _brandService.Update(ids, input);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand.Message);
        }
        return Ok(updateBrand);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(List<long> ids)
    {
        var deleteBrand = await _brandService.Delete(ids);
        if (!deleteBrand.Success)
        {
            return BadRequest(deleteBrand.Message);
        }
        return Ok(deleteBrand.Message);
    }
}