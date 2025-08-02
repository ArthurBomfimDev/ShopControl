using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Brand;
using ShopControl.Domain.Service.Base;
using ShopControl.Infrastructure.Interface.ValidateService;

namespace ShopControl.Infrastructure.Application;

public class BrandValidateService : BaseValidate_1<BrandValidateDTO, InputCreateBrand, InputUpdateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand>, IBrandValidateService
{

    #region Create
    public void ValidateCreate(List<BrandValidateDTO> listBrandValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listBrandValidate)
         where i.InputCreate == null
         let SetInvalid = i.SetIgnore()
         select InvalidNull(listBrandValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.RepeatedInputCreateCode != null
         let setInvalid = i.SetInvalid()
         select RepeatedIdentifier(i.InputCreate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.OriginalBrandDTO != null
         let setInvalid = i.SetInvalid()
         select AlreadyExists(i.InputCreate.Code, "Código")).ToList();

        ValidateLenght(listBrandValidate);

        //(from i in RemoveIgnore(listBrandValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputCreate.Name, 1, 64)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreate!.Code, i.InputCreate.Name, 1, 64, "Nome") : NullField(i.InputCreate.Code, "Nome")).ToList();

        //(from i in RemoveIgnore(listBrandValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputCreate.Code, 1, 6)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreate!.Code, i.InputCreate.Code, 1, 6, "Código") : NullField(i.InputCreate.Code, "Código")).ToList();

        //(from i in RemoveIgnore(listBrandValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputCreate.Description, 0, 100)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputCreate!.Code, i.InputCreate.Description, 0, 100, "Descrição") : NullField(i.InputCreate.Code, "Descrição")).ToList();

        (from i in RemoveInvalid(listBrandValidate)
         select SuccessfullyRegistered(i.InputCreate.Code, "Marca")).ToList();
    }
    #endregion

    #region Update
    public void ValidateUpdate(List<BrandValidateDTO> listBrandValidate)
    {
        CreateDictionary();

        (from i in listBrandValidate
         where i.InputIdentityUpdate == null || i.InputIdentityUpdate.InputUpdate == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listBrandValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.OriginalBrandDTO == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentityUpdate.InputUpdate.Code, "Marca", i.InputIdentityUpdate.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.RepetedInputUpdateIdentity != 0
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.RepetedCode != null
         let setInvalid = i.SetInvalid()
         select RepeatedIdentifier(i.InputIdentityUpdate.InputUpdate.Code, "Código")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where !i.Invalid && i.CodeExists != null && i.CodeExists != i.OriginalBrandDTO?.Code
         let setInvalid = i.SetInvalid()
         select AlreadyExists(i.InputIdentityUpdate.InputUpdate.Code, "Código")).ToList();

        ValidateLenght(listBrandValidate);

        //(from i in RemoveIgnore(listBrandValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputIdentityUpdate.InputUpdate.Name, 1, 24)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Name, 1, 24, "Nome") : NullField(i.InputIdentityUpdate.InputUpdate.Code, "Nome")).ToList();

        //(from i in RemoveIgnore(listBrandValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputIdentityUpdate.InputUpdate.Code, 1, 6)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Code, 1, 6, "Código") : NullField(i.InputIdentityUpdate.InputUpdate.Code, "Código")).ToList();

        //(from i in RemoveIgnore(listBrandValidate)
        // let resultInvalidLenght = InvalidLenghtValidate(i.InputIdentityUpdate.InputUpdate.Description, 0, 100)
        // where resultInvalidLenght != EnumValidateType.Valid
        // let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
        // select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.InputUpdate.Description, 0, 100, "Descrição") : NullField(i.InputIdentityUpdate.InputUpdate.Code, "Descrição")).ToList();

        (from i in RemoveInvalid(listBrandValidate)
         select SuccessfullyUpdated(i.InputIdentityUpdate.InputUpdate.Code, i.InputIdentityUpdate.Id, "Marca")).ToList();
    }
    #endregion

    #region Delete
    public void ValidateDelete(List<BrandValidateDTO> listBrandValidate)
    {
        CreateDictionary();

        (from i in RemoveIgnore(listBrandValidate)
         where i.RepeteInputDelete != null
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentityDelete.Id.ToString(), i.InputIdentityDelete.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.OriginalBrandDTO == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentityDelete.Id.ToString(), "Marca", i.InputIdentityDelete.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.BrandWithProduct != 0
         let setInvalid = i.SetInvalid()
         select LikedValue(i.InputIdentityDelete.Id.ToString(), "Produto(s)", "Marca")).ToList();

        (from i in RemoveInvalid(listBrandValidate)
         select SuccessfullyDeleted(i.InputIdentityDelete.Id.ToString(), "Marca")).ToList();
    }
    #endregion
}