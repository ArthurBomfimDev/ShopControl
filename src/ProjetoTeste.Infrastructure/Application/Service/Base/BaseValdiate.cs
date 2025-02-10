using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.Response;
using ProjetoTeste.Arguments.Arguments.Base.Validate;
using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ProjetoTeste.Infrastructure.Application.Service.Base;

public class BaseValdiate<TValidateDTO> where TValidateDTO : BaseValidateDTO
{

    #region Base
    public static List<TValidateDTO> RemoveInvalid(this List<TValidateDTO> listValidateDTO) => (from i in listValidateDTO where !i.Invalid select i).ToList();
    #endregion

    #region Validate

    #region InvalidLength
    public static EnumValidateType InvalidLenght(string? value, int minLeght, int MaxLenght)
    {
        if (string.IsNullOrWhiteSpace(value))
            return minLeght == 0 ? EnumValidateType.Valid : EnumValidateType.Invalid;

        int lenght = value.Length;

        if (lenght > MaxLenght)
            return EnumValidateType.Invalid;

        return EnumValidateType.Valid;
    }
    #endregion

    #region CPFValidate
    public static EnumValidateType CPFValidate(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return EnumValidateType.NotInformed;

        cpf = Regex.Replace(cpf, "[^0-9]", string.Empty); //Substitui todo q não é digito por uma string vazia Ex = 123.456.789-09

        if (cpf.Length != 11) return EnumValidateType.Invalid;

        if (new string(cpf[0], cpf.Length) == cpf) return EnumValidateType.Invalid; // Verifica se o cpf é composto pelo primeiro digito repetido

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores1[i]; // (cpf[i] - '0') -> jeito de converter um caracter em um digito numerico (substitui o valor unico) ex '0' = valor unicode 48 -> '0': = 48 - 48 = 0
        }

        int resto = soma % 11;
        int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto; // (operador ternário)  Verifica o primeiro digito verificador se é menor que dois, ser for é igual a 0, se for maior que dois a conta é (11 - resto)

        if (cpf[9] - '0' != primeiroDigitoVerificador) return EnumValidateType.Invalid; //verificar se o o primeiro digito veriicador é igual o primeiro

        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores2[i];
        }

        resto = soma % 11;
        int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

        if (cpf[10] - '0' != segundoDigitoVerificador) return EnumValidateType.Invalid;

        return EnumValidateType.Valid;
    }
    #endregion

    #region EmailValidate
    public static EnumValidateType EmailValidate(string email)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) return EnumValidateType.NotInformed;
        try
        {
            var validate = new MailAddress(email);
            return EnumValidateType.Valid;
        }
        catch (FormatException)
        {
            return EnumValidateType.Invalid;
        }
    }
    #endregion

    #endregion

    #region Notifiction
    public static class NotifactionMessages
    {
        public const string InvalidLenghtKey = "InvalidLenght";
    }

    public bool InvalidLenght(string identifier, string? value, int minLeght, int maxLenght, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, NotifactionMessages.InvalidLenghtKey)
    }

    #endregion

    #region Helper
    private bool AddToDictionary(string key, DetailedNotification detailedNotification)
    {
        NotificationHelper.Add(key, detailedNotification);
        return true;
    }


    private bool HandleValidation(string key, EnumValidateType validateType, string invalidMessage, string nonInformedMessage)
    {
        if (EnumValidateType.Invalid == validateType)
        {
            AddToDictionary(key, new DetailedNotification(key, [invalidMessage], EnumNotificationType.Error));
            return true;
        }
        if (EnumValidateType.NotInformed == validateType)
        {
            AddToDictionary(key, new DetailedNotification(key, [nonInformedMessage], EnumNotificationType.Error));
            return true;
        }
        return false;
    }
    #endregion
}