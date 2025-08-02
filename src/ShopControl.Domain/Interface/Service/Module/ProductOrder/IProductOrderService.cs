using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Base.Crud;
using ShopControl.Arguments.Arguments.ProductOrder;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service.Base;

namespace ShopControl.Domain.Interface.Service.Module.ProductOrder;

public interface IProductOrderService : IBaseService<ProductOrderDTO, InputCreateProductOrder, BaseInputUpdate_0, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, BaseInputIdentityView_0, OutputProductOrder>
{
}