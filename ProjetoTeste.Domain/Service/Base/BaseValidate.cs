using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Enum.Validate;
using ProjetoTeste.Domain.Helper;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ProjetoTeste.Domain.Service.Base;

public class BaseValidate<TValidateDTO> where TValidateDTO : BaseValidateDTO
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

    #region CPFValidate
    public static EnumValidateType CPFValidate(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return EnumValidateType.NonInformed;

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
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) return EnumValidateType.NonInformed;
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

    #region NotifcationKey
    internal static class NotifactionKey
    {
        public const string InvalidLenghtKey = "InvalidLenght";
        public const string NullFieldKey = "NullField";
        public const string RepeatedIdentifierKey = "RepetedIdentifier";
        public const string AlreadyExistsKey = "AlreadyExists";
        public const string InvalidRecordKey = "InvalidRecord";
        public const string SuccessfullyRegisteredKey = "SuccessfullyRegistered";
        public const string SuccessfullyUpdatedKey = "SuccessfullyUpdated";
        public const string SuccessfullyDeletedKey = "SuccessfullyDeleted";
        public const string InvalidNullKey = "InvalidNull";
        public const string NotFoundIdKey = "NotFoundId";
        public const string RepetaedIdKey = "RepeatedId";
        public const string LikedValueKey = "LinkedValue";
    }
    #endregion

    #region NotficationMessage
    public static class NotificationMessage
    {
        public static string InvalidLenghtMessage(string fieldName, string? value, int minLeght, int MaxLenght) => $"ERROR KEY: {NotifactionKey.InvalidLenghtKey.ToString()} - O Campo: {fieldName}, com valor: '{value}' deve ter entre {minLeght} e {MaxLenght} caracters.";
        public static string NullFieldMessage(string fieldName) => $"ERROR KEY: {NotifactionKey.NullFieldKey.ToString()} - O Campo: {fieldName} está vazio.";
        public static string InvalidNullMessage(int index) => $"ERROR KEY: {NotifactionKey.InvalidNullKey.ToString()} - O item na posição {index + 1} está vazio e não pode ser cadastrado.";
        public static string RepeatedIdentifier(string fieldName, string? value) => $"ERROR KEY: {NotifactionKey.RepeatedIdentifierKey.ToString()} - O Campo identificador: {fieldName}, com valor '{value}', foi insirido repetidas vezes e não pode ser cadastrado.";
        public static string RepeatedId(long Id) => $"ERROR KEY: {NotifactionKey.RepetaedIdKey.ToString()} - O Id: {Id}, foi insirido repetidas vezes e não pode ser utilizado.";
        public static string AlreadyExists(string fieldName, string? value) => $"ERROR KEY: {NotifactionKey.AlreadyExistsKey.ToString()} - O Campo identificador: {fieldName}, com valor '{value}', não pode ser utilizado, pois já está em uso.";
        public static string NotFoundId(string className, long id) => $"ERROR KEY: {NotifactionKey.NotFoundIdKey.ToString()} - {className} com Id: {id} não pode ser utilizado(a), pois não foi encontrado.";
        public static string LikedValue(string className, string likedValue) => $"ERROR KEY: {NotifactionKey.LikedValueKey.ToString()} - {className} não pode ser deletado(a), pois possui {likedValue} atrelado.";
        public static string SuccessfullyRegistered(string className, string idetifier) => $"SUCCESS: {NotifactionKey.SuccessfullyRegisteredKey.ToString()} - {className} com identifcador: '{idetifier}' foi cadastrado(a) com sucesso.";
        public static string SuccessfullyUpdated(string className, string idetifier, long id) => $"SUCCESS: {NotifactionKey.SuccessfullyUpdatedKey.ToString()} - {className} com identifcador: '{idetifier}' e Id: {id} foi atualizado(a) com sucesso.";
        public static string SuccessfullyDeleted(string className, string id) => $"SUCCESS: {NotifactionKey.SuccessfullyDeletedKey.ToString()} - {className} com Id: {id} foi deletado(a) com sucesso.";
    }
    #endregion

    #region MessageGeneration
    public bool InvalidNull(int index)
    {
        return AddErrorMessage(index.ToString(), NotificationMessage.InvalidNullMessage(index));
    }

    public bool InvalidLenght(string identifier, string? value, int minLeght, int maxLenght, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, NotifactionKey.InvalidLenghtKey, NotificationMessage.InvalidLenghtMessage(propertyName, value, minLeght, maxLenght), NotifactionKey.NullFieldKey, NotificationMessage.NullFieldMessage(propertyName));
    }

    public bool RepeatedIdentifier(string identifier, string fieldName)
    {
        return AddErrorMessage(identifier, NotificationMessage.RepeatedIdentifier(fieldName, identifier));
    }

    public bool AlreadyExists(string identifier, string fieldName)
    {
        return AddErrorMessage(identifier, NotificationMessage.AlreadyExists(fieldName, identifier));
    }


    public bool NotFoundId(string idetifier, string className, long id)
    {
        return AddErrorMessage(idetifier, NotificationMessage.NotFoundId(className, id));
    }

    public bool RepeatedId(string idetifier, long id)
    {
        return AddErrorMessage(idetifier, NotificationMessage.RepeatedId(id));
    }

    public bool LikedValue(string idetifier, string linkedValue, string className)
    {
        return AddErrorMessage(idetifier, NotificationMessage.LikedValue(className, linkedValue));
    }

    public bool SuccessfullyRegistered(string idetifier, string className)
    {
        return AddSuccessMessage(idetifier, NotificationMessage.SuccessfullyRegistered(className, idetifier));
    }

    public bool SuccessfullyUpdated(string idetifier, long id, string className)
    {
        return AddSuccessMessage(idetifier, NotificationMessage.SuccessfullyUpdated(className, idetifier, id));
    }

    public bool SuccessfullyDeleted(string idetifier, string className)
    {
        return AddSuccessMessage(idetifier, NotificationMessage.SuccessfullyDeleted(className, idetifier));
    }
    #endregion

    #endregion

    #region Helper
    private bool AddToDictionary(string key, DetailedNotification detailedNotification)
    {
        NotificationHelper.Add(key, detailedNotification);
        return true;
    }

    public bool AddErrorMessage(string key, string message)
    {
        return AddToDictionary(key, new DetailedNotification(key, [message], EnumNotificationType.Error));
    }

    public bool AddSuccessMessage(string key, string message)
    {
        return AddToDictionary(key, new DetailedNotification(key, [message], EnumNotificationType.Success));
    }


    private bool HandleValidation(string key, EnumValidateType validateType, string KeyInvalid, string invalidMessage, string KeyNonInformed, string nonInformedMessage)
    {
        if (EnumValidateType.Invalid == validateType)
        {
            AddToDictionary(key, new DetailedNotification(key, [invalidMessage], EnumNotificationType.Error));
            return true;
        }
        if (EnumValidateType.NonInformed == validateType)
        {
            AddToDictionary(key, new DetailedNotification(key, [nonInformedMessage], EnumNotificationType.Error));
            return true;
        }
        return false;
    }

    public (List<DetailedNotification> Successes, List<DetailedNotification> Error) GetValidationResult()
    {
        var successes = NotificationHelper.Get().Where(i => i.NotificationType == EnumNotificationType.Success).ToList();
        var errors = NotificationHelper.Get().Where(i => i.NotificationType != EnumNotificationType.Success).ToList();

        return (successes, errors);
    }
    #endregion
}