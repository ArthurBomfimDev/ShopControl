using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments;
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
    public async Task<ActionResult<OutputBrand?>> Get(InputIdentifyViewBrand inputIdentifyViewBrand)
    {
        var brand = await _brandService.Get(inputIdentifyViewBrand);
        return Ok(brand);
    }

    [HttpPost("Id/Multiple")]
    public async Task<ActionResult<List<OutputBrand>?>> GetListByListId(List<InputIdentifyViewBrand> listInputIdentifyViewBrand)
    {
        var brand = await _brandService.GetListByListId(listInputIdentifyViewBrand);
        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<List<OutputBrand>>>> Create(InputCreateBrand inputCreateBrand)
    {
        var createdBrand = await _brandService.Create(inputCreateBrand);
        if (!createdBrand.Success)
        {
            return BadRequest(createdBrand);
        }
        return Ok(createdBrand);
    }

    [HttpPost("Multiple")]
    public async Task<ActionResult<BaseResponse<List<OutputBrand>>>> Create(List<InputCreateBrand> listInputCreateBrand)
    {
        var createdBrand = await _brandService.CreateMultiple(listInputCreateBrand);
        if (!createdBrand.Success)
        {
            return BadRequest(createdBrand);
        }
        return Ok(createdBrand);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResponse<bool>>> Update(InputIdentityUpdateBrand InputIdentityUpdateBrand)
    {
        var updateBrand = await _brandService.Update(InputIdentityUpdateBrand);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand);
        }
        return Ok(updateBrand);
    }

    [HttpPut("Multiple")]
    public async Task<ActionResult<BaseResponse<bool>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var updateBrand = await _brandService.UpdateMultiple(listInputIdentityUpdateBrand);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand);
        }
        return Ok(updateBrand);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(InputIdentifyDeleteBrand inputIdentifyDeleteBrand)
    {
        var deleteBrand = await _brandService.Delete(inputIdentifyDeleteBrand);
        if (!deleteBrand.Success)
        {
            return BadRequest(deleteBrand);
        }
        return Ok(deleteBrand);
    }

    [HttpDelete("Multiple")]
    public async Task<ActionResult> Delete(List<InputIdentifyDeleteBrand> listInputIdentifyDeleteBrand)
    {
        var deleteBrand = await _brandService.DeleteMultiple(listInputIdentifyDeleteBrand);
        if (!deleteBrand.Success)
        {
            return BadRequest(deleteBrand);
        }
        return Ok(deleteBrand);
    }
}