using AutoMapper;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Conversor.BasesConversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service.Base;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Application.Service.Base;

public abstract class BaseService<TIRepository, TEntity, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO> : IBaseService<TEntity, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO>
    where TEntity : BaseEntity
    where TInputCreateDTO : BaseInputCreate<TInputCreateDTO>
    where TInputIdentityUpdateDTO : BaseInputIdentityUpdate<TInputIdentityUpdateDTO>
    where TInputIdentityDeleteDTO : BaseInputIdentityDelete<TInputIdentityDeleteDTO>
    where TInputIdentityViewDTO : BaseInputIdentityView<TInputIdentityViewDTO>, IBaseIdentity
    where TOutputDTO : BaseOutput<TOutputDTO>
    where TIRepository : IRepository<TEntity>
{
    private readonly TIRepository _repository;
    private readonly IMapper _mapper;

    public BaseService(TIRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #region Get
    public virtual async Task<TOutputDTO> Get(TInputIdentityViewDTO inputIdentifyViewDTO)
    {
        var get = await _repository.Get(inputIdentifyViewDTO.Id);
        return get.Converter<TEntity, TOutputDTO>();
    }

    public virtual async Task<List<TOutputDTO>> GetAll()
    {
        var getAll = await _repository.GetAll();
        return getAll.ConverterList<TEntity, TOutputDTO>();
    }

    public virtual async Task<List<TOutputDTO>> GetListByListId(List<TInputIdentityViewDTO> listTInputIdentityViewDTO)
    {
        var getListByListId = await _repository.GetListByListId(listTInputIdentityViewDTO.Select(i => i.Id).ToList());
        return getListByListId.ConverterList<TEntity, TOutputDTO>();
    }
    #endregion

    #region Create
    public virtual async Task<BaseResponse<TOutputDTO>> Create(TInputCreateDTO inputCreateDTO)
    {
        var response = new BaseResponse<TOutputDTO>();

        var result = await CreateMultiple([inputCreateDTO]);

        response.Message = result.Message;
        response.Success = result.Success;

        if (!response.Success)
            return response;

        response.Content = result.Content.FirstOrDefault();
        return response;
    }

    public virtual Task<BaseResponse<List<TOutputDTO>>> CreateMultiple(List<TInputCreateDTO> listInputCreateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Update
    public virtual async Task<BaseResponse<bool>> Update(TInputIdentityUpdateDTO inputIdentityUpdateDTO)
    {
        return await UpdateMultiple([inputIdentityUpdateDTO]);
    }

    public virtual Task<BaseResponse<bool>> UpdateMultiple(List<TInputIdentityUpdateDTO> listInputIdentityUpdateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Delete
    public virtual async Task<BaseResponse<bool>> Delete(TInputIdentityDeleteDTO inputIdentifyDeleteDTO)
    {
        return await DeleteMultiple([inputIdentifyDeleteDTO]);
    }

    public virtual Task<BaseResponse<bool>> DeleteMultiple(List<TInputIdentityDeleteDTO> listInputIdentityDeleteDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

}