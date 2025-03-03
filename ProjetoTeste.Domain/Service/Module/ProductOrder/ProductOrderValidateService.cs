using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Service.Module.ProductOrder;
using ProjetoTeste.Domain.Service.Base;

namespace ProjetoTeste.Domain.Service.Module.ProductOrder;

public class ProductOrderValidateService : BaseValidate<ProductOrderValidateDTO>, IProductOrderValidateService
{
    #region ValidateStock
    private bool ValidateStock(List<ProductDTO> listProduct, long value, string identifier, long productId)
    {
        if (value <= listProduct.FirstOrDefault(j => j.Id == productId).Stock)
        {
            listProduct.FirstOrDefault(j => j.Id == productId).Stock -= value;
            return true;
        }
        else
        {
            UnavailableStock(identifier, value, listProduct.FirstOrDefault(j => j.Id == productId).Stock);
            return false;
        }
    }
    #endregion

    public void ValidateCreate(List<ProductOrderValidateDTO> listProductOrderValidate)
    {
        CreateDictionary();

        (from i in listProductOrderValidate
         where i.OrderDTO == null
         let setInvalid = i.SetInvalid()
         select InvalidRelatedProperty(i.InputCreateProductOrder.OrderId.ToString(), "Id do Pedido", i.InputCreateProductOrder.OrderId)).ToList();

        (from i in listProductOrderValidate
         where i.Product == null
         let setIgnore = i.SetIgnore()
         select InvalidRelatedProperty(i.InputCreateProductOrder.OrderId.ToString(), "Produto", i.InputCreateProductOrder.ProductId)).ToList();

        (from i in RemoveIgnore(listProductOrderValidate)
         where i.InputCreateProductOrder.Quantity < 1
         let setInvalid = i.SetInvalid()
         select InvalidOrderValueLess(i.InputCreateProductOrder.OrderId.ToString(), i.InputCreateProductOrder.Quantity, 1)).ToList();

        var listProduct = RemoveIgnore(listProductOrderValidate).Select(i => i.Product).ToList();


        (from i in RemoveIgnore(listProductOrderValidate)
         let value = i.InputCreateProductOrder.Quantity
         select ValidateStock(listProduct, value, i.InputCreateProductOrder.OrderId.ToString(), i.InputCreateProductOrder.ProductId) == true ? true : i.SetInvalid()).ToList();
        //foreach (var i in RemoveIgnore(listProductOrderValidate))
        //{
        //    if (!i.Invalid && i.InputCreateProductOrder.Quantity <= listProduct.FirstOrDefault(j => j.Id == i.InputCreateProductOrder.ProductId).Stock)
        //        listProduct.FirstOrDefault(j => j.Id == i.InputCreateProductOrder.ProductId).Stock -= i.InputCreateProductOrder.Quantity;
        //    else
        //    {
        //        i.SetInvalid();
        //        UnavailableStock(i.InputCreateProductOrder.OrderId.ToString(), i.InputCreateProductOrder.Quantity, listProduct.FirstOrDefault(j => j.Id == i.InputCreateProductOrder.ProductId).Stock);
        //    }
        //}

        (from i in RemoveInvalid(listProductOrderValidate)
         select SuccessfullyRegistered(i.OrderDTO.Id.ToString(), "Pedido de Produto")).ToList();
    }

    #region Ignore
    public void ValidateDelete(List<ProductOrderValidateDTO> listTValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void ValidateUpdate(List<ProductOrderValidateDTO> listTValidateDTO)
    {
        throw new NotImplementedException();
    }
    #endregion
}