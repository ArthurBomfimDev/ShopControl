using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service.Base;

public interface IBaseService<TEntity, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO>
    where TEntity : BaseEntity
    where TInputCreateDTO : BaseInputCreate<TInputCreateDTO>
    where TInputIdentityUpdateDTO : BaseInputIdentityUpdate<TInputIdentityUpdateDTO>
    where TInputIdentityDeleteDTO : BaseInputIdentityDelete<TInputIdentityDeleteDTO>
    where TInputIdentityViewDTO : BaseInputIdentityView<TInputIdentityViewDTO>, IBaseIdentity
    where TOutputDTO : BaseOutput<TOutputDTO>
{
    Task<List<TOutputDTO>> GetAll();
    Task<TOutputDTO> Get(TInputIdentityViewDTO inputIdentifyViewDTO);
    Task<List<TOutputDTO>> GetListByListId(List<TInputIdentityViewDTO> listTInputIdentityViewDTO);
    Task<BaseResponse<TOutputDTO>> Create(TInputCreateDTO inputCreateDTO);
    Task<BaseResponse<List<TOutputDTO>>> CreateMultiple(List<TInputCreateDTO> listInputCreateDTO);
    Task<BaseResponse<bool>> Update(TInputIdentityUpdateDTO inputIdentityUpdateDTO);
    Task<BaseResponse<bool>> UpdateMultiple(List<TInputIdentityUpdateDTO> listInputIdentityUpdateDTO);
    Task<BaseResponse<bool>> Delete(TInputIdentityDeleteDTO inputIdentifyDeleteDTO);
    Task<BaseResponse<bool>> DeleteMultiple(List<TInputIdentityDeleteDTO> listInputIdentityDeleteDTO);
}