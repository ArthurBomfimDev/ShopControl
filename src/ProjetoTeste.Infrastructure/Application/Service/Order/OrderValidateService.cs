using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Infrastructure.Application.Service.Order;

public class OrderValidateService : IOrderValidateService
{
    #region Create Order
    public async Task<BaseResponse<List<OrderValidate>>> CreateValidateOrder(List<OrderValidate> listOrderValidate)
    {
        var response = new BaseResponse<List<OrderValidate>>();
        _ = (from i in listOrderValidate
             where i.CustomerId == 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Pedido do Cliente com id: {i.InputCreateOrder.CustomerId} não foi cadastrado, pois ele não existe")
             select i).ToList();

        var create = (from i in listOrderValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage($"Pedido do cliente com id: {i.InputCreateOrder.CustomerId} foi cadastrado com sucesso")
                      select i).ToList();

        if (!create.Any())
        {
            response.Success = false;
            return response;
        }
        response.Content = create;
        return response;
    }
    #endregion

    #region Create ProductOrder
    public async Task<BaseResponse<List<ProductOrderValidate>>> CreateValidateProductOrder(List<ProductOrderValidate> listProductOrderValidate)
    {
        var response = new BaseResponse<List<ProductOrderValidate>>();
        _ = (from i in listProductOrderValidate
             where i.OrderId == default
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O pedido com o Id: '{i.OrderId}' não pode ser processado, pois o pedido não foi encontrado. Verifique os dados inseridos.")
             select i).ToList();

        _ = (from i in listProductOrderValidate
             where i.Product == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O pedido com o Id: '{i.OrderId}' e Id do produto '{i.InputCreateProductOrder.ProductId}' não pode ser processado, pois o produto não foi encontrado. Verifique os dados inseridos.")
             select i).ToList();

        _ = (from i in listProductOrderValidate
             where !i.Invalid && i.InputCreateProductOrder.Quantity < 1
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O pedido com o Id: '{i.OrderId}' e Id do produto '{i.InputCreateProductOrder.ProductId}' não pode ser processado, pois a quantidade: solicitada ({i.InputCreateProductOrder.Quantity}) é inválida. A quantidade: mínima permitida é 1.")
             select i).ToList();

        var listProduct = listProductOrderValidate.Select(i => i.Product).ToList();

        foreach (var i in listProductOrderValidate)
        {
            if (!i.Invalid && i.InputCreateProductOrder.Quantity < listProduct.FirstOrDefault(j => j.Id == i.InputCreateProductOrder.ProductId).Stock)
                listProduct.FirstOrDefault(j => j.Id == i.InputCreateProductOrder.ProductId).Stock -= i.InputCreateProductOrder.Quantity;
            else
            {
                i.SetInvalid();
                response.AddErrorMessage($"O pedido com o Id: '{i.OrderId}' e Id do produto '{i.InputCreateProductOrder.ProductId}' não pode ser processado, pois a quantidade: solicitada ({i.InputCreateProductOrder.Quantity}) excede o estoque disponível ({listProduct.FirstOrDefault(j => j.Id == i.InputCreateProductOrder.ProductId).Stock}).");
            }
        }

        var create = (from i in listProductOrderValidate
                      where !i.Invalid
                      select i).ToList();

        if (!create.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = create;
        return response;
    }
    #endregion
}