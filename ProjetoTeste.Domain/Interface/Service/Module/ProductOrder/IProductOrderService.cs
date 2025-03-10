using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.Crud;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Domain.Interface.Service.Module.ProductOrder;

public interface IProductOrderService : IBaseService<ProductOrderDTO, InputCreateProductOrder, BaseInputUpdate_0, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, BaseInputIdentityView_0, OutputProductOrder>
{
}