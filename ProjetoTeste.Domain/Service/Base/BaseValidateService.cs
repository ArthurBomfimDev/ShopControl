using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Domain.Service.Base;
public class BaseValidateService<TValidateDTO> : BaseValidate<TValidateDTO>, IBaseValidateService<TValidateDTO>
    where TValidateDTO : BaseValidateDTO
{
    public void ValidateCreate(List<TValidateDTO> listTValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void ValidateDelete(List<TValidateDTO> listTValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void ValidateUpdate(List<TValidateDTO> listTValidateDTO)
    {
        throw new NotImplementedException();
    }
}