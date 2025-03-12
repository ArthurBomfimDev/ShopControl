using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.Crud;
using ProjetoTeste.Arguments.Enum.Validate;
using ProjetoTeste.Domain.DTO.Base;
using System.ComponentModel.DataAnnotations;

namespace ProjetoTeste.Domain.Service.Base;

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
                                 where Attribute.IsDefined(i, typeof(RequiredAttribute))
                                 select i.Name).FirstOrDefault();

        (from i in RemoveIgnore(listValidateDTO) // Possivel alteração passar parametro de Update ou Create (ou fazer automatico) mas para isso seria necessario transformar implicitamente em DTO
         let input = CreateOrUpdate(i)
         where input != null && i.DictionaryLength != null
         let identificator = input.GetType().GetProperty(identificatorName!).GetValue(input).ToString()
         from j in i.DictionaryLength!
         let propertyValue = input.GetType().GetProperty(j.Key).GetValue(input).ToString()
         let resultInvalidLenght = InvalidLenghtValidate(propertyValue, j.Value[0], j.Value[1])
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = i.SetInvalid()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(identificator, propertyValue, j.Value[0], j.Value[1], j.Key) : NullField(identificator, j.Key.ToString())).ToList();
    }

    public dynamic CreateOrUpdate(TValidateDTO validateDTO)
    {
        return validateDTO.InputCreate == null ? validateDTO.InputIdentityUpdate.InputUpdate : validateDTO.InputCreate;
    }
}