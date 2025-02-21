using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IOrderValidateService : IBaseValidateService<OrderValidateDTO>
{
    Task<BaseResponse<List<ProductOrderValidateDTO>>> CreateValidateProductOrder(List<ProductOrderValidateDTO> listProductOrderValidate);
}