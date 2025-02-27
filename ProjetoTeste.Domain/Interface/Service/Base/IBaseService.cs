using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Domain.DTO.Base;

namespace ProjetoTeste.Domain.Interface.Service.Base;

public interface IBaseService<TDTO, TInputCreateDTO, TInputIdentityUpdateDTO, TInputIdentityDeleteDTO, TInputIdentityViewDTO, TOutputDTO>
    where TDTO : BaseDTO<TDTO>
    where TInputCreateDTO : BaseInputCreate<TInputCreateDTO>
    where TInputIdentityUpdateDTO : BaseInputIdentityUpdate<TInputIdentityUpdateDTO>
    where TInputIdentityDeleteDTO : BaseInputIdentityDelete<TInputIdentityDeleteDTO>
    where TInputIdentityViewDTO : BaseInputIdentityView<TInputIdentityViewDTO>, IBaseIdentity
    where TOutputDTO : BaseOutput<TOutputDTO>
{
    Task<List<TOutputDTO>> GetAll();
    Task<TOutputDTO> Get(TInputIdentityViewDTO inputIdentifyViewDTO);
    Task<List<TOutputDTO>> GetListByListId(List<TInputIdentityViewDTO> listTInputIdentityViewDTO);
    Task<BaseResult<TOutputDTO>> Create(TInputCreateDTO inputCreateDTO);
    Task<BaseResult<List<TOutputDTO>>> CreateMultiple(List<TInputCreateDTO> listInputCreateDTO);
    Task<BaseResult<bool>> Update(TInputIdentityUpdateDTO inputIdentityUpdateDTO);
    Task<BaseResult<bool>> UpdateMultiple(List<TInputIdentityUpdateDTO> listInputIdentityUpdateDTO);
    Task<BaseResult<bool>> Delete(TInputIdentityDeleteDTO inputIdentifyDeleteDTO);
    Task<BaseResult<bool>> DeleteMultiple(List<TInputIdentityDeleteDTO> listInputIdentityDeleteDTO);
}