using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IOrderValidateService
{
    Task<BaseResponse<List<OrderValidateDTO>>> CreateValidateOrder(List<OrderValidateDTO> listOrderValidate);
    Task<BaseResponse<List<ProductOrderValidateDTO>>> CreateValidateProductOrder(List<ProductOrderValidateDTO> listProductOrderValidate);
}