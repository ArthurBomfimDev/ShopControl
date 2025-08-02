using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Base.ApiResponse;
using ShopControl.Arguments.Arguments.Base.Crud;
using ShopControl.Domain.DTO.Base;
using ShopControl.Domain.Interface.Service.Base;
using ShopControl.Infrastructure.Interface.UnitOfWork;
using ShopControl.Infrastructure.Persistence.Entity.Base;

namespace ShopControl.Api.Controllers.Base;

[Route("api/v1/[controller]")]
[ApiController]
public abstract class BaseController<TService, TDTO, TEntity, TInputCreate, TInputUpdate, TInputIndetityUpdate, TInputIndetityDelete, TInputIndeityView, TOutput> : Controller
    where TService : IBaseService<TDTO, TInputCreate, TInputUpdate, TInputIndetityUpdate, TInputIndetityDelete, TInputIndeityView, TOutput>
    where TEntity : BaseEntity
    where TDTO : BaseDTO<TDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIndetityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIndetityDelete : BaseInputIdentityDelete<TInputIndetityDelete>
    where TInputIndeityView : BaseInputIdentityView<TInputIndeityView>, IBaseIdentity
    where TOutput : BaseOutput<TOutput>
{
    #region Dependecy Injection
    private readonly IUnitOfWork _unitOfWork;
    private readonly TService _service;

    public BaseController(IUnitOfWork unitOfWork, TService service)
    {
        _unitOfWork = unitOfWork;
        _service = service;
    }
    #endregion

    #region Transacation
    public override void OnActionExecuting(ActionExecutingContext context) //overide substitui o comportamento padrão do controller
    {
        _unitOfWork.BeginTransaction();
        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _unitOfWork.Commit();
        base.OnActionExecuted(context);
    }
    #endregion

    #region Get
    [HttpGet("Get/All")]
    public virtual async Task<ActionResult<List<TOutput>>> GetAll()
    {
        var getAll = await _service.GetAll();
        return Ok(getAll);
    }

    [HttpPost("Get/Id")]
    public virtual async Task<ActionResult<TOutput>> Get(TInputIndeityView inputIdentifyView)
    {
        var get = await _service.Get(inputIdentifyView);
        return Ok(get);
    }

    [HttpPost("Get/ListByListId")]
    public virtual async Task<ActionResult<TOutput>> GetListByListId(List<TInputIndeityView> listInputIdentifyView)
    {
        var getListByListId = await _service.GetListByListId(listInputIdentifyView);
        return Ok(getListByListId);
    }
    #endregion

    #region Create
    [HttpPost("Create")]
    public virtual async Task<ActionResult<BaseResult<List<TOutput>>>> Create(TInputCreate inputCreateDTO)
    {
        var create = await _service.Create(inputCreateDTO);
        return Ok(create);
    }

    [HttpPost("Create/Multiple")]
    public virtual async Task<ActionResult<BaseResult<List<TOutput>>>> Create(List<TInputCreate> listTInputCreateDTO)
    {
        var create = await _service.CreateMultiple(listTInputCreateDTO);
        if (create.IsSuccess == false)
        {
            return BadRequest(create);
        }
        return Ok(create);
    }
    #endregion

    #region Update
    [HttpPut("Update")]
    public virtual async Task<ActionResult<BaseResult<bool>>> Update(TInputIndetityUpdate inputIdentityUpdateDTO)
    {
        var update = await _service.Update(inputIdentityUpdateDTO);
        if (!update.IsSuccess)
        {
            return BadRequest(update);
        }
        return Ok(update);
    }

    [HttpPut("Update/Multiple")]
    public virtual async Task<ActionResult<BaseResult<bool>>> Update(List<TInputIndetityUpdate> listTInputIndetityUpdate)
    {
        var listUpdate = await _service.UpdateMultiple(listTInputIndetityUpdate);
        if (!listUpdate.IsSuccess)
        {
            return BadRequest(listUpdate);
        }
        return Ok(listUpdate);
    }
    #endregion

    #region Delete
    [HttpDelete("Delete")]
    public virtual async Task<ActionResult<BaseResponse<bool>>> Delete(TInputIndetityDelete inputIdentifyDeleteDTO)
    {
        var delete = await _service.Delete(inputIdentifyDeleteDTO);
        if (!delete.IsSuccess)
        {
            return BadRequest(delete);
        }
        return Ok(delete);
    }

    [HttpDelete("Delete/Multiple")]
    public virtual async Task<ActionResult<BaseResponse<bool>>> Delete(List<TInputIndetityDelete> listInputIdentifyDeleteDTO)
    {
        var listDelete = await _service.DeleteMultiple(listInputIdentifyDeleteDTO);
        if (!listDelete.IsSuccess)
        {
            return BadRequest(listDelete);
        }
        return Ok(listDelete);
    }
    #endregion
}