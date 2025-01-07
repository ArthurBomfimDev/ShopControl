namespace ProjetoTeste.Arguments.Arguments.Base;

public class BaseResponse<TContent>
{
    public bool Success { get; set; } = true;
    public List<string?> Message { get; set; } = new List<string?>();
    public TContent? Content { get; set; }
}