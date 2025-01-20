using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Application.Service.Order;

public class OrderValidateService
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

        _ = (from i in listProductOrderValidate
             where !i.Invalid && i.Product.Stock < i.InputCreateProductOrder.Quantity
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O pedido com o Id: '{i.OrderId}' e Id do produto '{i.InputCreateProductOrder.ProductId}' não pode ser processado, pois a quantidade: solicitada ({i.InputCreateProductOrder.Quantity}) excede o estoque disponível ({i.Product.Stock}).")
             select i).ToList();

        var create = (from i in listProductOrderValidate
                      where !i.Invalid
                      let stockUpdate = i.Product.Stock -= i.InputCreateProductOrder.Quantity
                      let message = response.AddSuccessMessage($"O pedido com Id: '{i.OrderId}' do produto com o Id: '{i.InputCreateProductOrder.ProductId}' e quantidade: '{i.InputCreateProductOrder.Quantity}' foi efetuado com sucesso.")
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