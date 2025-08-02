using ShopControl.Arguments.Arguments;
using ShopControl.Domain.Service.Base;
using ShopControl.Infrastructure.Interface.ValidateService;

namespace ShopControl.Infrastructure.Application.Service.Order;

public class OrderValidateService : BaseValidate<OrderValidateDTO>, IOrderValidateService
{
    #region Create
    public void ValidateCreate(List<OrderValidateDTO> listOrderValidate)
    {
        CreateDictionary();

        (from i in listOrderValidate
         where i.CustomerId == 0
         let setInvalid = i.SetInvalid()
         select InvalidRelatedProperty(i.CustomerId.ToString(), "Id do Usuário", i.CustomerId)).ToList();

        (from i in RemoveInvalid(listOrderValidate)
         select SuccessfullyRegistered(i.CustomerId.ToString(), "Pedido")).ToList();
    }
    #endregion

    #region Ignore
    public void ValidateDelete(List<OrderValidateDTO> listTValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void ValidateUpdate(List<OrderValidateDTO> listTValidateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion
}