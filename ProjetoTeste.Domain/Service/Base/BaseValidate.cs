using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Enum.Validate;
using ProjetoTeste.Domain.Notification;

namespace ProjetoTeste.Domain.Service.Base;

public class BaseValidate<TValidateDTO> : NotificationGeneration where TValidateDTO : BaseValidateDTO
{

    #region Base
    public static List<TValidateDTO> RemoveInvalid(List<TValidateDTO> listValidateDTO) => (from i in listValidateDTO where !i.Invalid select i).ToList();
    public static List<TValidateDTO> RemoveIgnore(List<TValidateDTO> listValidateDTO) => (from i in listValidateDTO where !i.Ignore select i).ToList();
    #endregion

    #region Validate

    #region InvalidLength
    public static EnumValidateType InvalidLenght(string? value, int minLeght, int MaxLenght)
    {
        if (string.IsNullOrWhiteSpace(value))
            return minLeght == 0 ? EnumValidateType.Valid : EnumValidateType.NonInformed;

        int lenght = value.Length;

        if (lenght > MaxLenght)
            return EnumValidateType.Invalid;

        return EnumValidateType.Valid;
    }
    #endregion

    #endregion

}