namespace ShopControl.Arguments.Arguments.Base.ApiResponse;

public class DetailedNotification(string? identifier, List<string>? listMessage, EnumNotificationType notificationType)
{
    public string? Identifier { get; set; } = identifier;
    public List<string>? ListMessage { get; set; } = listMessage;
    public EnumNotificationType NotificationType { get; set; } = notificationType;
}