using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Interface.Service.Base;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BaseController<TService, TEntity, TInputCreateDTO, TInputIndetityUpdate, TInputIndetityDelete, TInputIndeityViewDTO, TOutputDTO> : Controller
    where TService : IBaseService<TEntity, TInputCreateDTO, TInputIndetityUpdate, TInputIndetityDelete, TInputIndeityViewDTO, TOutputDTO>
    where TEntity : BaseEntity, new()
    where TInputIndeityViewDTO : IBaseIdentity
{
    #region Dependecy Injection
    private readonly IUnitOfWork unitOfWork;
    private readonly TService service;

    public BaseController(IUnitOfWork unitOfWork, TService service)
    {
        this.unitOfWork = unitOfWork;
        this.service = service;
    }
    #endregion

    #region Transacation
    public override void OnActionExecuting(ActionExecutingContext context) //overide substitui o comportamento padrão do controller
    {
        unitOfWork.BeginTransaction();
        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        unitOfWork.Commit();
        base.OnActionExecuted(context);
    }
    #endregion

    #region Get
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<TOutputDTO>>> GetAll()
    {
        var getAll = await service.GetAll();
        return Ok(getAll);
    }

    [HttpPost("Id")]
    public async Task<ActionResult<TOutputDTO>> Get(TInputIndeityViewDTO inputIdentifyView)
    {
        var get = await service.Get(inputIdentifyView);
        return Ok(get);
    }

    [HttpPost("GetListByListId")]
    public async Task<ActionResult<TOutputDTO>> GetListByListId(List<TInputIndeityViewDTO> listInputIdentifyView)
    {
        var getListByListId = await service.GetListByListId(listInputIdentifyView);
        return Ok(getListByListId);
    }
    #endregion

    #region Create
    [HttpPost("Create")]
    public async Task<ActionResult<BaseResponse<List<TOutputDTO>>>> Create(TInputCreateDTO inputCreateDTO)
    {
        var createDTO = await service.Create(inputCreateDTO);
        if (createDTO.Success == false)
        {
            return BadRequest(createDTO);
        }
        return Ok(createDTO);
    }

    [HttpPost("CreateMultiple")]
    public async Task<ActionResult<BaseResponse<List<TOutputDTO>>>> Create(List<TInputCreateDTO> listTInputCreateDTO)
    {
        var createDTO = await service.CreateMultiple(listTInputCreateDTO);
        if (createDTO.Success == false)
        {
            return BadRequest(createDTO);
        }
        return Ok(createDTO);
    }
    #endregion

    #region Update
    [HttpPut("Update")]
    public async Task<ActionResult<BaseResponse<bool>>> Update(TInputIndetityUpdate inputIdentityUpdateDTO)
    {
        var updateDTO = await service.Update(inputIdentityUpdateDTO);
        if (!updateDTO.Success)
        {
            return BadRequest(updateDTO);
        }
        return Ok(updateDTO);
    }

    [HttpPut("UpdateMultiple")]
    public async Task<ActionResult<BaseResponse<bool>>> Update(List<TInputIndetityUpdate> listTInputIndetityUpdate)
    {
        var listUpdateDTO = await service.UpdateMultiple(listTInputIndetityUpdate);
        if (!listUpdateDTO.Success)
        {
            return BadRequest(listUpdateDTO);
        }
        return Ok(listUpdateDTO);
    }
    #endregion

    #region Delete
    [HttpDelete("Delete")]
    public async Task<ActionResult<BaseResponse<bool>>> Delete(TInputIndetityDelete inputIdentifyDeleteDTO)
    {
        var deleteDTO = await service.Delete(inputIdentifyDeleteDTO);
        if (!deleteDTO.Success)
        {
            return BadRequest(deleteDTO);
        }
        return Ok(deleteDTO);
    }

    [HttpDelete("DeleteMultiple")]
    public async Task<ActionResult<BaseResponse<bool>>> Delete(List<TInputIndetityDelete> listInputIdentifyDeleteDTO)
    {
        var listDeleteDTO = await service.DeleteMultiple(listInputIdentifyDeleteDTO);
        if (!listDeleteDTO.Success)
        {
            return BadRequest(listDeleteDTO);
        }
        return Ok(listDeleteDTO);
    }
    #endregion
}