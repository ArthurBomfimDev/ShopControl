using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Base.ApiResponse;
using ShopControl.Arguments.Arguments.Base.Crud;
using ShopControl.Arguments.Conversor;
using ShopControl.Domain.DTO.Base;
using ShopControl.Domain.Interface.Repository.Base;
using ShopControl.Domain.Interface.Service.Base;

namespace ShopControl.Domain.Service.Base;

public abstract class BaseService<TIRepository, TValidateService, TDTO, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentityView, TOutput, TValidate> : BaseValidate<TValidate>, IBaseService<TDTO, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentityView, TOutput>
    where TDTO : BaseDTO<TDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
    where TInputIdentityView : BaseInputIdentityView<TInputIdentityView>, IBaseIdentity
    where TOutput : BaseOutput<TOutput>
    where TIRepository : IBaseRepository<TDTO>
    where TValidateService : IBaseValidateService<TValidate>
    where TValidate : BaseValidateDTO
{
    private readonly TIRepository _repository;
    private readonly TValidateService _validateService;

    public BaseService(TIRepository repository, TValidateService validateService)
    {
        _repository = repository;
        _validateService = validateService;
    }

    #region Get
    public virtual async Task<TOutput> Get(TInputIdentityView inputIdentifyViewDTO)
    {
        var get = await _repository.Get(inputIdentifyViewDTO.Id);
        return (dynamic)get;
    }

    public virtual async Task<List<TOutput>> GetAll()
    {
        var getAll = await _repository.GetAll();
        return getAll.ConverterList<TDTO, TOutput>();
    }

    public virtual async Task<List<TOutput>> GetListByListId(List<TInputIdentityView> listTInputIdentityViewDTO)
    {
        var getListByListId = await _repository.GetListByListId(listTInputIdentityViewDTO.Select(i => i.Id).ToList());
        return getListByListId.ConverterList<TDTO, TOutput>();
    }
    #endregion

    #region Create
    public virtual async Task<BaseResult<TOutput>> Create(TInputCreate inputCreateDTO)
    {
        var create = await CreateMultiple([inputCreateDTO]);

        return create.IsSuccess == true ? BaseResult<TOutput>.Success(create.Value.FirstOrDefault(), create.listNotification) : BaseResult<TOutput>.Failure(create.listNotification);
    }

    public virtual Task<BaseResult<List<TOutput>>> CreateMultiple(List<TInputCreate> listInputCreateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Update
    public virtual async Task<BaseResult<bool>> Update(TInputIdentityUpdate inputIdentityUpdateDTO)
    {
        return await UpdateMultiple([inputIdentityUpdateDTO]);
    }

    public virtual Task<BaseResult<bool>> UpdateMultiple(List<TInputIdentityUpdate> listInputIdentityUpdateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Delete
    public virtual async Task<BaseResult<bool>> Delete(TInputIdentityDelete inputIdentifyDeleteDTO)
    {
        return await DeleteMultiple([inputIdentifyDeleteDTO]);
    }

    public virtual Task<BaseResult<bool>> DeleteMultiple(List<TInputIdentityDelete> listInputIdentityDeleteDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

}