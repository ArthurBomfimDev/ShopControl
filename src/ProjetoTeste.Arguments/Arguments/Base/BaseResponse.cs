namespace ProjetoTeste.Arguments.Arguments.Base;

public class BaseResponse<TContent>
{
    public bool Success { get; set; } = true;
    public TContent? Content { get; set; }
    public List<Notification>? Message { get; set; } = new List<Notification>();  
}