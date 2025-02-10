namespace ProjetoTeste.Arguments.Arguments.Base.Response;

public class DetailedNotification(string? key, List<string>? listMessage, EnumNotificationType notificationType)
{
    public string? Key { get; set; } = key;
    public List<string>? ListMessage { get; set; } = listMessage;
    public EnumNotificationType NotificationType { get; set; } = notificationType;
}
