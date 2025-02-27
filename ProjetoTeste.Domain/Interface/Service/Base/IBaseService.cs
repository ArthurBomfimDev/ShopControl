using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Domain.DTO.Base;

namespace ProjetoTeste.Domain.Interface.Service.Base;

public interface IBaseService<TDTO, TInputCreate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentityView, TOutput>
    where TDTO : BaseDTO<TDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputIdentityUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
    where TInputIdentityView : BaseInputIdentityView<TInputIdentityView>, IBaseIdentity
    where TOutput : BaseOutput<TOutput>
{
    Task<List<TOutput>> GetAll();
    Task<TOutput> Get(TInputIdentityView inputIdentifyViewDTO);
    Task<List<TOutput>> GetListByListId(List<TInputIdentityView> listTInputIdentityViewDTO);
    Task<BaseResult<TOutput>> Create(TInputCreate inputCreateDTO);
    Task<BaseResult<List<TOutput>>> CreateMultiple(List<TInputCreate> listInputCreateDTO);
    Task<BaseResult<bool>> Update(TInputIdentityUpdate inputIdentityUpdateDTO);
    Task<BaseResult<bool>> UpdateMultiple(List<TInputIdentityUpdate> listInputIdentityUpdateDTO);
    Task<BaseResult<bool>> Delete(TInputIdentityDelete inputIdentifyDeleteDTO);
    Task<BaseResult<bool>> DeleteMultiple(List<TInputIdentityDelete> listInputIdentityDeleteDTO);
}