using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Enum.Validate;
using ProjetoTeste.Domain.Helper;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Infrastructure.Application;

public class BrandValidateService : BaseValidate<BrandValidateDTO>, IBrandValidateService
{

    #region Create
    public void ValidateCreate(List<BrandValidateDTO> listBrandValidate)
    {
        NotificationHelper.CreateDict();

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

        (from i in RemoveIgnore(listBrandValidate)
         let resultInvalidLenght = InvalidLenght(i.InputCreate.Name, 1, 64)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputCreate!.Code, i.InputCreate.Name, 1, 64, resultInvalidLenght, "Nome")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         let resultInvalidLenght = InvalidLenght(i.InputCreate.Code, 1, 6)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputCreate!.Code, i.InputCreate.Code, 1, 6, resultInvalidLenght, "Código")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         let resultInvalidLenght = InvalidLenght(i.InputCreate.Description, 0, 100)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputCreate!.Code, i.InputCreate.Description, 0, 100, resultInvalidLenght, "Descrição")).ToList();

        (from i in RemoveInvalid(listBrandValidate)
         select SuccessfullyRegistered(i.InputCreate.Code, "Marca")).ToList();
    }
    #endregion

    #region Update
    public void ValidateUpdate(List<BrandValidateDTO> listBrandValidate)
    {
        NotificationHelper.CreateDict();

        (from i in listBrandValidate
         where i.InputUpdate == null || i.InputUpdate.InputUpdateBrand == null
         let setIgnore = i.SetIgnore()
         select InvalidNull(listBrandValidate.IndexOf(i))).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.OriginalBrandDTO == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputUpdate.InputUpdateBrand.Code, "Marca", i.InputUpdate.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.RepetedInputUpdateIdentify != 0
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputUpdate.InputUpdateBrand.Code, i.InputUpdate.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.RepetedCode != null
         let setInvalid = i.SetInvalid()
         select RepeatedIdentifier(i.InputUpdate.InputUpdateBrand.Code, "Código")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where !i.Invalid && i.CodeExists != null && i.CodeExists != i.OriginalBrandDTO?.Code
         let setInvalid = i.SetInvalid()
         select AlreadyExists(i.InputUpdate.InputUpdateBrand.Code, "Código")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         let resultInvalidLenght = InvalidLenght(i.InputUpdate.InputUpdateBrand.Name, 1, 24)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputUpdate.InputUpdateBrand.Code, i.InputUpdate.InputUpdateBrand.Name, 1, 24, resultInvalidLenght, "Nome")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         let resultInvalidLenght = InvalidLenght(i.InputUpdate.InputUpdateBrand.Code, 1, 6)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputUpdate.InputUpdateBrand.Code, i.InputUpdate.InputUpdateBrand.Code, 1, 6, resultInvalidLenght, "Código")).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         let resultInvalidLenght = InvalidLenght(i.InputUpdate.InputUpdateBrand.Description, 0, 100)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputUpdate.InputUpdateBrand.Code, i.InputUpdate.InputUpdateBrand.Description, 0, 100, resultInvalidLenght, "Descrição")).ToList();

        (from i in RemoveInvalid(listBrandValidate)
         select SuccessfullyUpdated(i.InputUpdate.InputUpdateBrand.Code, i.InputUpdate.Id, "Marca")).ToList();
    }
    #endregion

    #region Delete
    public void ValidateDelete(List<BrandValidateDTO> listBrandValidate)
    {
        NotificationHelper.CreateDict();

        (from i in RemoveIgnore(listBrandValidate)
         where i.RepeteInputDelete != null
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentifyDeleteBrand.Id.ToString(), i.InputIdentifyDeleteBrand.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.OriginalBrandDTO == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentifyDeleteBrand.Id.ToString(), "Marca", i.InputIdentifyDeleteBrand.Id)).ToList();

        (from i in RemoveIgnore(listBrandValidate)
         where i.BrandWithProduct != 0
         let setInvalid = i.SetInvalid()
         select LikedValue(i.InputIdentifyDeleteBrand.Id.ToString(), "Produto(s)", "Marca")).ToList();

        (from i in RemoveInvalid(listBrandValidate)
         select SuccessfullyDeleted(i.InputIdentifyDeleteBrand.Id.ToString(), "Marca")).ToList();
    }
    #endregion
}