namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Response<TValue>
{
    public bool Success { get; set; } = true;
    public List<string?> Message { get; set; } = new List<string?>();
    public TValue? Value { get; set; }
}