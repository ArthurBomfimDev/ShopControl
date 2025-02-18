using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Domain.Service.Base;
public class BaseValidateService<TValidateDTO> : BaseValdiate<TValidateDTO>, IBaseValidateService
    where TValidateDTO : BaseValidateDTO
{

}