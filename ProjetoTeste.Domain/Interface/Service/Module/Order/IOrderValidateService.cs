using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IOrderValidateService : IBaseValidateService
{
    Task<BaseResponse<List<OrderValidateDTO>>> CreateValidateOrder(List<OrderValidateDTO> listOrderValidate);
    Task<BaseResponse<List<ProductOrderValidateDTO>>> CreateValidateProductOrder(List<ProductOrderValidateDTO> listProductOrderValidate);
}