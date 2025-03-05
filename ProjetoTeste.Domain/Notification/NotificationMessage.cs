namespace ProjetoTeste.Domain.Notification;

public static class NotificationMessage
{
    #region Error
    public static string InvalidLenght(string fieldName, string? value, int minLeght, int MaxLenght) => $"ERROR KEY: InvalidLenghtKey - O Campo: {fieldName}, com valor: '{value}' deve ter entre {minLeght} e {MaxLenght} caracters.";

    public static string NullField(string fieldName) => $"ERROR KEY: NullField - O Campo: {fieldName} está vazio.";

    public static string InvalidNull(int index) => $"ERROR KEY: InvalidNull - O item na posição {index + 1} está vazio e não pode ser cadastrado.";

    public static string RepeatedIdentifier(string fieldName, string? value) => $"ERROR KEY: RepeatedIdentifier - O Campo identificador: {fieldName}, com valor '{value}', foi insirido repetidas vezes e não pode ser cadastrado.";

    public static string RepeatedId(long Id) => $"ERROR KEY: RepetaedId - O Id: {Id}, foi insirido repetidas vezes e não pode ser utilizado.";

    public static string AlreadyExists(string fieldName, string? value) => $"ERROR KEY: AlreadyExists - O Campo identificador: {fieldName}, com valor '{value}', não pode ser utilizado, pois já está em uso.";

    public static string NotFoundId(string className, long id) => $"ERROR KEY: NotFoundId - {className} com Id: {id} não pode ser utilizado(a), pois não foi encontrado.";

    public static string LikedValue(string className, string likedValue) => $"ERROR KEY: LikedValue - {className} não pode ser deletado(a), pois possui {likedValue} atrelado.";

    public static string InvalidCPF(string CpfValue) => $"ERROR KEY: InvalidCPF - O Cpf: {CpfValue} não pode ser utilizado, pois está invalido.";

    public static string InvalidEmail(string EmailValue) => $"ERROR KEY: InvalidEmail -  O Email: {EmailValue} não pode ser utilizado, pois está invalido.";

    public static string InvalidPhone(string phoneValue) => $"ERROR KEY: InvalidPhone -  O Número de Telefone: {phoneValue} não pode ser utilizado, pois está invalido.";

    public static string NegativeStock(long stock) => $"ERROR KEY: NegativeStock - Estoque: {stock}, Não pode ser utilizado por ser negativo";

    public static string NegativePrice(decimal price) => $"ERROR KEY: NegativePrice- Preço: {price.ToString("F2")}, Não pode ser utilizado por ser negativo";

    public static string InvalidRelatedProperty(string fieldName, long relatedId) => $"ERROR KEY: InvalidRelatedProperty - A Propriedade Ralacional {fieldName}: {relatedId}, não pode ser utilizado, por não existir.";

    public static string InvalidOrderValueLess(long value, long min) => $"ERROR KEY: InvalidOrderValueLess - Você não pode realizar esse pedido pois o valor pedido: {value} tem que ser maior ou igual a {min}.";

    public static string UnavailableStock(long orderedValue, long stock) => $"ERROR KEY: UnavailableStock - Pedido não pode ser processado, pois a quantidade solicitada {orderedValue} exece o estoque disponível: {stock}.";

    #endregion

    #region Success
    public static string SuccessfullyRegistered(string className, string idetifier) => $"SUCCESS: SuccessfullyRegistered - {className} com identifcador: '{idetifier}' foi cadastrado(a) com sucesso.";

    public static string SuccessfullyUpdated(string className, string idetifier, long id) => $"SUCCESS: SuccessfullyUpdated - {className} com identifcador: '{idetifier}' e Id: {id} foi atualizado(a) com sucesso.";

    public static string SuccessfullyDeleted(string className, string id) => $"SUCCESS: SuccessfullyDeleted - {className} com Id: {id} foi deletado(a) com sucesso.";
    #endregion
}