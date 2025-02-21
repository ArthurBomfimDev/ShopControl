using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Conversor;
using ProjetoTeste.Domain.DTO.Base;
using ProjetoTeste.Domain.Interface.Repository.Base;
using ProjetoTeste.Domain.Interface.Service.Base;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.Service.Base;

namespace ProjetoTeste.Infrastructure.Application.Service.Base;

public abstract class BaseService<TIRepository, TValidateService, TDTO, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO, TValidateDTO> : BaseValidate<TValidateDTO>, IBaseService<TDTO, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO>
    where TDTO : BaseDTO<TDTO>
    where TInputCreateDTO : BaseInputCreate<TInputCreateDTO>
    where TInputIdentityUpdateDTO : BaseInputIdentityUpdate<TInputIdentityUpdateDTO>
    where TInputIdentityDeleteDTO : BaseInputIdentityDelete<TInputIdentityDeleteDTO>
    where TInputIdentityViewDTO : BaseInputIdentityView<TInputIdentityViewDTO>, IBaseIdentity
    where TOutputDTO : BaseOutput<TOutputDTO>
    where TIRepository : IBaseRepository<TDTO>
    where TValidateService : IBaseValidateService<TValidateDTO>
    where TValidateDTO : BaseValidateDTO
{
    private readonly TIRepository _repository;
    private readonly TValidateService _validateService;

    public BaseService(TIRepository repository, TValidateService validateService)
    {
        _repository = repository;
        _validateService = validateService;
    }

    #region Get
    public virtual async Task<TOutputDTO> Get(TInputIdentityViewDTO inputIdentifyViewDTO)
    {
        var get = await _repository.Get(inputIdentifyViewDTO.Id);
        return get.Converter<TDTO, TOutputDTO>();
    }

    public virtual async Task<List<TOutputDTO>> GetAll()
    {
        var getAll = await _repository.GetAll();
        return getAll.ConverterList<TDTO, TOutputDTO>();
    }

    public virtual async Task<List<TOutputDTO>> GetListByListId(List<TInputIdentityViewDTO> listTInputIdentityViewDTO)
    {
        var getListByListId = await _repository.GetListByListId(listTInputIdentityViewDTO.Select(i => i.Id).ToList());
        return getListByListId.ConverterList<TDTO, TOutputDTO>();
    }
    #endregion

    #region Create
    public virtual async Task<BaseResult<TOutputDTO>> Create(TInputCreateDTO inputCreateDTO)
    {
        throw new NotImplementedException();
        //var result = await CreateMultiple([inputCreateDTO]);
    }

    public virtual Task<BaseResult<List<TOutputDTO>>> CreateMultiple(List<TInputCreateDTO> listInputCreateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Update
    public virtual async Task<BaseResult<bool>> Update(TInputIdentityUpdateDTO inputIdentityUpdateDTO)
    {
        return await UpdateMultiple([inputIdentityUpdateDTO]);
    }

    public virtual Task<BaseResult<bool>> UpdateMultiple(List<TInputIdentityUpdateDTO> listInputIdentityUpdateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Delete
    public virtual async Task<BaseResult<bool>> Delete(TInputIdentityDeleteDTO inputIdentifyDeleteDTO)
    {
        return await DeleteMultiple([inputIdentifyDeleteDTO]);
    }

    public virtual Task<BaseResult<bool>> DeleteMultiple(List<TInputIdentityDeleteDTO> listInputIdentityDeleteDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

}