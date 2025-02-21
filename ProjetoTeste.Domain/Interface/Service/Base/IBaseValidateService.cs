using ProjetoTeste.Arguments.Arguments;

namespace ProjetoTeste.Domain.Interface.Service.Base;

public interface IBaseValidateService<TValidateDTO> where TValidateDTO : BaseValidateDTO
{
    void ValidateCreate(List<TValidateDTO> listTValidateDTO);
    void ValidateUpdate(List<TValidateDTO> listTValidateDTO);
    void ValidateDelete(List<TValidateDTO> listTValidateDTO);
}