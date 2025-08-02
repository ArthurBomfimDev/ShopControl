using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Base.Crud;
using ShopControl.Arguments.DataAnnotation;
using ShopControl.Arguments.Enum.Validate;
using ShopControl.Domain.DTO.Base;
using System.ComponentModel.DataAnnotations;

namespace ShopControl.Domain.Service.Base;

public class BaseValidate_1<TValidateDTO, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete> : BaseValidate<TValidateDTO>
    where TValidateDTO : BaseValidateDTO_1<TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{

    public void ValidateLenght(List<TValidateDTO> listValidateDTO)
    {
        var identificatorName = (from i in typeof(TInputCreate).GetProperties()
                                 where Attribute.IsDefined(i, typeof(IdentifierAttribute))
                                 select i.Name).FirstOrDefault();

        (from i in RemoveIgnore(listValidateDTO) // Possivel alteração passar parametro de Update ou Create (ou fazer automatico) mas para isso seria necessario transformar implicitamente em DTO
         let inputProperty = CreateOrUpdate(i)
         where inputProperty != null && i.DictionaryLength != null
         let identificator = inputProperty.GetType().GetProperty(identificatorName!).GetValue(inputProperty).ToString()
         from j in i.DictionaryLength!
         let propertyValue = inputProperty.GetType().GetProperty(j.Key).GetValue(inputProperty).ToString()
         let resultInvalidLenght = InvalidLenghtValidate(propertyValue, j.Value[0], j.Value[1])
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = i.SetInvalid()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(identificator, propertyValue, j.Value[0], j.Value[1], j.Key) : NullField(identificator, j.Key.ToString())).ToList();
    }

    public dynamic? CreateOrUpdate(TValidateDTO validateDTO)
    {
        return validateDTO!.InputCreate == null ? validateDTO.InputIdentityUpdate.InputUpdate : validateDTO.InputCreate;
    }
}