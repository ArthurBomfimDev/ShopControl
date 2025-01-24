using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IOrderValidateService
{
    Task<BaseResponse<List<OrderValidate>>> CreateValidateOrder(List<OrderValidate> listOrderValidate);
    Task<BaseResponse<List<ProductOrderValidate>>> CreateValidateProductOrder(List<ProductOrderValidate> listProductOrderValidate);
}