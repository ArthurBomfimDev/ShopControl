using AutoMapper;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service.Base;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Application.Service.Base;

public class BaseService<TIRepository, TEntity, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO> : IBaseService<TEntity, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO>
    where TEntity : BaseEntity, new()
    where TInputIdentityViewDTO : IBaseIdentity
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
    public async Task<TOutputDTO> Get(TInputIdentityViewDTO inputIdentifyViewDTO)
    {
        var get = await _repository.Get(inputIdentifyViewDTO.Id);
        return _mapper.Map<TOutputDTO>(get);
    }

    public async Task<List<TOutputDTO>> GetAll()
    {
        var getAll = await _repository.GetAll();
        return _mapper.Map<List<TOutputDTO>>(getAll);
    }

    public async Task<List<TOutputDTO>> GetListByListId(List<TInputIdentityViewDTO> listTInputIdentityViewDTO)
    {
        var getListByListId = await _repository.GetListByListId(listTInputIdentityViewDTO.Select(i => i.Id).ToList());
        return _mapper.Map<List<TOutputDTO>>(getListByListId);
    }
    #endregion

    #region Create
    public virtual Task<BaseResponse<TOutputDTO>> Create(TInputCreateDTO inputCreateDTO)
    {
        throw new NotImplementedException();
    }

    public virtual Task<BaseResponse<List<TOutputDTO>>> CreateMultiple(List<TInputCreateDTO> listInputCreateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Update
    public virtual Task<BaseResponse<bool>> Update(TInputIdentityUpdateDTO inputIdentityUpdateDTO)
    {
        throw new NotImplementedException();
    }

    public virtual Task<BaseResponse<bool>> UpdateMultiple(List<TInputIdentityUpdateDTO> listInputIdentityUpdateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Delete
    public virtual Task<BaseResponse<bool>> Delete(TInputIdentityDeleteDTO inputIdentifyDeleteDTO)
    {
        throw new NotImplementedException();
    }

    public virtual Task<BaseResponse<bool>> DeleteMultiple(List<TInputIdentityDeleteDTO> listInputIdentityDeleteDTO)
    {
        throw new NotImplementedException();
    }
    #endregion

}