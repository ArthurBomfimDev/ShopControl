using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Product;
using ShopControl.Domain.Service.Base;
using ShopControl.Infrastructure.Interface.ValidateService;

namespace ShopControl.Infrastructure.Application;

public class ProductValidateService : BaseValidate_1<ProductValidateDTO, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct>, IProductValidateService
{
    #region Create
    public void ValidateCreate(List<ProductValidateDTO> listProductValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputCreate == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listProductValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.OriginalCode != null
         let setInvalid = i.SetInvalid()
         select AlreadyExists(i.InputCreate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepeteCode != null
         let setInvalid = i.SetInvalid()
         select RepeatedIdentifier(i.InputCreate.Code, "Código")).ToList();

        //(from i in RemoveIgnore(listProductValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputCreate.Code, 1, 6)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreate.Code, i.InputCreate.Code, 1, 6, "Código") : NullField(i.InputCreate.Code, "Código")).ToList();

        //(from i in RemoveIgnore(listProductValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputCreate.Name, 1, 24)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreate.Code, i.InputCreate.Name, 1, 24, "Nome") : NullField(i.InputCreate.Code, "Nome")).ToList();

        //(from i in RemoveIgnore(listProductValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputCreate.Description, 0, 100)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreate.Code, i.InputCreate.Description, 0, 100, "Descrição") : NullField(i.InputCreate.Code, "Descrição")).ToList();

        ValidateLenght(listProductValidate);

        (from i in RemoveIgnore(listProductValidate)
         where i.BrandId == 0
         let setInvalid = i.SetInvalid()
         select InvalidRelatedProperty(i.InputCreate.Code, "Id da Marca", i.InputCreate.BrandId)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputCreate.Price < 0
         let setInvalid = i.SetInvalid()
         select NegativePrice(i.InputCreate.Code, i.InputCreate.Price)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputCreate.Stock < 0
         let setInvalid = i.SetInvalid()
         select NegativeStock(i.InputCreate.Code, i.InputCreate.Stock)).ToList();

        (from i in RemoveInvalid(listProductValidate)
         select SuccessfullyRegistered(i.InputCreate.Code, "Produto")).ToList();

    }
    #endregion

    #region Update
    public void ValidateUpdate(List<ProductValidateDTO> listProductValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentityUpdate == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listProductValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepeteIdentity != 0
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.Original == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentityUpdate.InputUpdate.Code, "Marca", i.InputIdentityUpdate.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepeteCode != null
         let setInvalid = i.SetInvalid()
         select RepeatedIdentifier(i.InputIdentityUpdate.InputUpdate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.OriginalCode != null && i.Original.Code != i.InputIdentityUpdate.InputUpdate.Code
         let setInvalid = i.SetInvalid()
         select AlreadyExists(i.InputIdentityUpdate.InputUpdate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.BrandId == default(long)
         let setInvalid = i.SetInvalid()
         select InvalidRelatedProperty(i.InputIdentityUpdate.InputUpdate.Code, "Id da Marca", i.InputIdentityUpdate.InputUpdate.BrandId)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentityUpdate.InputUpdate.Price < 0
         let setInvalid = i.SetInvalid()
         select NegativePrice(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Price)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentityUpdate.InputUpdate.Stock < 0
         let setInvalid = i.SetInvalid()
         select NegativePrice(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Stock)).ToList();


        ValidateLenght(listProductValidate);

        //(from i in RemoveIgnore(listProductValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputIdentityUpdate.InputUpdate.Name, 1, 24)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Name, 1, 24, "Nome") : NullField(i.InputIdentityUpdate.InputUpdate.Code, "Nome")).ToList();

        //(from i in RemoveIgnore(listProductValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputIdentityUpdate.InputUpdate.Code, 1, 6)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Code, 1, 6, "Código") : NullField(i.InputIdentityUpdate.InputUpdate.Code, "Código")).ToList();

        //(from i in RemoveIgnore(listProductValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputIdentityUpdate.InputUpdate.Description, 0, 100)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Description, 0, 100, "Descrição") : NullField(i.InputIdentityUpdate.InputUpdate.Code, "Descrição")).ToList();


        (from i in RemoveInvalid(listProductValidate)
         select SuccessfullyUpdated(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.Id, "Produto")).ToList();
    }
    #endregion

    #region Delete
    public void ValidateDelete(List<ProductValidateDTO> listProductValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listProductValidate)
         where i.InputIdentityDelete == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listProductValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.RepetedIdentity != 0
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentityDelete.Id.ToString(), i.InputIdentityDelete.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where i.Original == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentityDelete.Id.ToString(), "Produto", i.InputIdentityDelete.Id)).ToList();

        (from i in RemoveIgnore(listProductValidate)
         where !i.Invalid && i.Original.Stock > 0
         let setInvald = i.SetInvalid()
         select LikedValue(i.InputIdentityDelete.Id.ToString(), "Estoque", "Produto")).ToList();

        (from i in RemoveInvalid(listProductValidate)
         select SuccessfullyDeleted(i.InputIdentityDelete.Id.ToString(), "Produto")).ToList();
    }
    #endregion

}