namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Response<TValue>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public TValue? Value { get; set; }
}