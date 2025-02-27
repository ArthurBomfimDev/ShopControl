namespace ProjetoTeste.Domain.Notification;

public static class NotificationMessage
{
    #region Error
    public static string InvalidLenght(string fieldName, string? value, int minLeght, int MaxLenght) => $"ERROR KEY: {NotificationKey.InvalidLenghtKey.ToString()} - O Campo: {fieldName}, com valor: '{value}' deve ter entre {minLeght} e {MaxLenght} caracters.";

    public static string NullField(string fieldName) => $"ERROR KEY: {NotificationKey.NullFieldKey.ToString()} - O Campo: {fieldName} está vazio.";

    public static string InvalidNull(int index) => $"ERROR KEY: {NotificationKey.InvalidNullKey.ToString()} - O item na posição {index + 1} está vazio e não pode ser cadastrado.";

    public static string RepeatedIdentifier(string fieldName, string? value) => $"ERROR KEY: {NotificationKey.RepeatedIdentifierKey.ToString()} - O Campo identificador: {fieldName}, com valor '{value}', foi insirido repetidas vezes e não pode ser cadastrado.";

    public static string RepeatedId(long Id) => $"ERROR KEY: {NotificationKey.RepetaedIdKey.ToString()} - O Id: {Id}, foi insirido repetidas vezes e não pode ser utilizado.";

    public static string AlreadyExists(string fieldName, string? value) => $"ERROR KEY: {NotificationKey.AlreadyExistsKey.ToString()} - O Campo identificador: {fieldName}, com valor '{value}', não pode ser utilizado, pois já está em uso.";

    public static string NotFoundId(string className, long id) => $"ERROR KEY: {NotificationKey.NotFoundIdKey.ToString()} - {className} com Id: {id} não pode ser utilizado(a), pois não foi encontrado.";

    public static string LikedValue(string className, string likedValue) => $"ERROR KEY: {NotificationKey.LikedValueKey.ToString()} - {className} não pode ser deletado(a), pois possui {likedValue} atrelado.";

    public static string InvalidCPF(string CpfValue) => $"ERROR KEY: {NotificationKey.InvalidCPFKey.ToString()} - O Cpf: {CpfValue} não pode ser utilizado, pois está invalido.";

    public static string InvalidEmail(string EmailValue) => $"ERROR KEY: {NotificationKey.InvalidEmailKey.ToString()} -  O Email: {EmailValue} não pode ser utilizado, pois está invalido.";

    public static string InvalidPhone(string phoneValue) => $"ERROR KEY: {NotificationKey.InvalidPhoneKey.ToString()} -  O Número de Telefone: {phoneValue} não pode ser utilizado, pois está invalido.";

    public static string NegativeStock(long stock) => $"ERROR KEY: {NotificationKey.NegativeStockKey.ToString()} - Estoque: {stock}, Não pode ser utilizado por ser negativo";

    public static string NegativePrice(decimal price) => $"ERROR KEY: {NotificationKey.NegativePriceKey.ToString()} - Preço: {price.ToString("F2")}, Não pode ser utilizado por ser negativo";

    public static string InvalidRelatedProperty(string fieldName, long relatedId) => $"ERROR KEY: {NotificationKey.InvalidRelatedPropertyKey.ToString()} - A Propriedade Ralacional {fieldName}: {relatedId}, não pode ser utilizado, por não existir.";

    #endregion

    #region Success
    public static string SuccessfullyRegistered(string className, string idetifier) => $"SUCCESS: {NotificationKey.SuccessfullyRegisteredKey.ToString()} - {className} com identifcador: '{idetifier}' foi cadastrado(a) com sucesso.";

    public static string SuccessfullyUpdated(string className, string idetifier, long id) => $"SUCCESS: {NotificationKey.SuccessfullyUpdatedKey.ToString()} - {className} com identifcador: '{idetifier}' e Id: {id} foi atualizado(a) com sucesso.";

    public static string SuccessfullyDeleted(string className, string id) => $"SUCCESS: {NotificationKey.SuccessfullyDeletedKey.ToString()} - {className} com Id: {id} foi deletado(a) com sucesso.";
    #endregion
}