using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Enum.Validate;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Infrastructure.Application;

public class ProductValidateService : BaseValidate<ProductValidateDTO>, IProductValidateService
{
    #region Create
    public void ValidateCreate(List<ProductValidateDTO> listProductValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputCreateProduct == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listProductValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.OriginalCode != null
         let setInvalid = i.SetInvalid()
         select AlreadyExists(i.InputCreateProduct.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepeteCode != null
         let setInvalid = i.SetInvalid()
         select RepeatedIdentifier(i.InputCreateProduct.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         let resultInvalidLenght = InvalidLenght(i.InputCreateProduct.Code, 1, 6)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreateProduct.Code, i.InputCreateProduct.Code, 1, 6, "Código") : NullField(i.InputCreateProduct.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         let resultInvalidLenght = InvalidLenght(i.InputCreateProduct.Name, 1, 24)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreateProduct.Code, i.InputCreateProduct.Name, 1, 24, "Nome") : NullField(i.InputCreateProduct.Code, "Nome")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         let resultInvalidLenght = InvalidLenght(i.InputCreateProduct.Description, 0, 100)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreateProduct.Code, i.InputCreateProduct.Description, 0, 100, "Descrição") : NullField(i.InputCreateProduct.Code, "Descrição")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.BrandId == 0
         let setInvalid = i.SetInvalid()
         select InvalidRelatedProperty(i.InputCreateProduct.Code, "Id da Marca", i.InputCreateProduct.BrandId)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputCreateProduct.Price < 0
         let setInvalid = i.SetInvalid()
         select NegativePrice(i.InputCreateProduct.Code, i.InputCreateProduct.Price)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputCreateProduct.Stock < 0
         let setInvalid = i.SetInvalid()
         select NegativeStock(i.InputCreateProduct.Code, i.InputCreateProduct.Stock)).ToList();

        (from i in RemoveInvalid(listProductValidate)
         select SuccessfullyRegistered(i.InputCreateProduct.Code, "Produto")).ToList();

    }
    #endregion

    #region Update
    public void ValidateUpdate(List<ProductValidateDTO> listProductValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentityUpdateProduct == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listProductValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepeteIdentity != 0
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentityUpdateProduct.InputUpdate.Code, i.InputIdentityUpdateProduct.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.Original == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentityUpdateProduct.InputUpdate.Code, "Marca", i.InputIdentityUpdateProduct.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepeteCode != null
         let setInvalid = i.SetInvalid()
         select RepeatedIdentifier(i.InputIdentityUpdateProduct.InputUpdate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.OriginalCode != null && i.Original.Code != i.InputIdentityUpdateProduct.InputUpdate.Code
         let setInvalid = i.SetInvalid()
         select AlreadyExists(i.InputIdentityUpdateProduct.InputUpdate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.BrandId == default(long)
         let setInvalid = i.SetInvalid()
         select InvalidRelatedProperty(i.InputIdentityUpdateProduct.InputUpdate.Code, "Id da Marca", i.InputIdentityUpdateProduct.InputUpdate.BrandId)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentityUpdateProduct.InputUpdate.Price < 0
         let setInvalid = i.SetInvalid()
         select NegativePrice(i.InputIdentityUpdateProduct.InputUpdate.Code, i.InputIdentityUpdateProduct.InputUpdate.Price)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentityUpdateProduct.InputUpdate.Stock < 0
         let setInvalid = i.SetInvalid()
        select NegativePrice(i.InputIdentityUpdateProduct.InputUpdate.Code, i.InputIdentityUpdateProduct.InputUpdate.Stock)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         let resultInvalidLenght = InvalidLenght(i.InputIdentityUpdateProduct.InputUpdate.Name, 1, 24)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdateProduct.InputUpdate.Code, i.InputIdentityUpdateProduct.InputUpdate.Name, 1, 24, "Nome") : NullField(i.InputIdentityUpdateProduct.InputUpdate.Code, "Nome")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         let resultInvalidLenght = InvalidLenght(i.InputIdentityUpdateProduct.InputUpdate.Code, 1, 6)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdateProduct.InputUpdate.Code, i.InputIdentityUpdateProduct.InputUpdate.Code, 1, 6, "Código") : NullField(i.InputIdentityUpdateProduct.InputUpdate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         let resultInvalidLenght = InvalidLenght(i.InputIdentityUpdateProduct.InputUpdate.Description, 0, 100)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdateProduct.InputUpdate.Code, i.InputIdentityUpdateProduct.InputUpdate.Description, 0, 100, "Descrição") : NullField(i.InputIdentityUpdateProduct.InputUpdate.Code, "Descrição")).ToList();


        (from i in RemoveInvalid(listProductValidate)
         select SuccessfullyUpdated(i.InputIdentityUpdateProduct.InputUpdate.Code, i.InputIdentityUpdateProduct.Id, "Produto")).ToList();
    }
    #endregion

    #region Delete
    public void ValidateDelete(List<ProductValidateDTO> listProductValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentifyDeleteProduct == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listProductValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepetedIdentity != 0
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentifyDeleteProduct.Id.ToString(), i.InputIdentifyDeleteProduct.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.Original == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentifyDeleteProduct.Id.ToString(), "Produto", i.InputIdentifyDeleteProduct.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where !i.Invalid && i.Original.Stock > 0
         let setInvald = i.SetInvalid()
         select LikedValue(i.InputIdentifyDeleteProduct.Id.ToString(), "Estoque", "Produto")).ToList();

        (from i in RemoveInvalid(listProductValidate)
         select SuccessfullyDeleted(i.InputIdentifyDeleteProduct.Id.ToString(), "Produto")).ToList();
    }
    #endregion

}