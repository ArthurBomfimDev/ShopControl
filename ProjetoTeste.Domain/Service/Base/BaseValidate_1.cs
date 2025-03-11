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

    public void CreateValidateLenght(List<TValidateDTO> listValidateDTO)
    {
        var identificatorName = (from i in typeof(TInputCreate).GetProperties()
                                 where Attribute.IsDefined(i, typeof(RequiredAttribute))
                                 select i.Name).FirstOrDefault();

        (from i in RemoveIgnore(listValidateDTO) // Possivel alteração passar parametro de Update ou Create (ou fazer automatico) mas para isso seria necessario transformar implicitamente em DTO
         where i.InputCreate != null && i.DictionaryLength != null
         let identificator = i.InputCreate.GetType().GetProperty(identificatorName!).GetValue(i.InputCreate).ToString()
         from j in i.DictionaryLength!
         let propertyValue = i.InputCreate.GetType().GetProperty(j.Key).GetValue(i.InputCreate).ToString()
         let resultInvalidLenght = InvalidLenghtValidate(propertyValue, j.Value[0], j.Value[1])
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = i.SetInvalid()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(identificator, propertyValue, j.Value[0], j.Value[1], j.Key) : NullField(identificator, j.Key.ToString())).ToList();
    }

    public void UpdateValidateLenght(List<TValidateDTO> listValidateDTO)
    {
        var identificatorName = (from i in typeof(TInputCreate).GetProperties()
                                 where Attribute.IsDefined(i, typeof(RequiredAttribute))
                                 select i.Name).FirstOrDefault();

        (from i in RemoveIgnore(listValidateDTO)
         where i.InputIdentityUpdate.InputUpdate != null && i.DictionaryLength != null
         let identificator = i.InputIdentityUpdate.InputUpdate.GetType().GetProperty(identificatorName!).GetValue(i.InputIdentityUpdate.InputUpdate).ToString()
         from j in i.DictionaryLength!
         let propertyValue = i.InputIdentityUpdate.InputUpdate.GetType().GetProperty(j.Key).GetValue(i.InputIdentityUpdate.InputUpdate).ToString()
         let resultInvalidLenght = InvalidLenghtValidate(propertyValue, j.Value[0], j.Value[1])
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = i.SetInvalid()
         select resultInvalidLenght == EnumValidateType.Invalid ? InvalidLenght(identificator, propertyValue, j.Value[0], j.Value[1], j.Key) : NullField(identificator, j.Key.ToString())).ToList();
    }

}