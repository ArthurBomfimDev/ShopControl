namespace ProjetoTeste.Arguments.Arguments.Base;

public class BaseResponse<TContent>
{
    public bool Success { get; set; } = true;
    public TContent? Content { get; set; }
    public List<Notification> Message { get; set; } = new List<Notification>();
    public bool AddSuccessMessage(string message)
    {

        Message.Add(new Notification(message, EnumNotificationType.Success));
        return true;
    }
    public bool AddErrorMessage(string message)
    {
        Message.Add(new Notification(message, EnumNotificationType.Error));
        return true;
    }
    public bool AddAlertMessage(string message)
    {
        Message.Add(new Notification(message, EnumNotificationType.Alert));
        return true;
    }
}