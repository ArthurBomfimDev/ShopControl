using Microsoft.AspNetCore.Mvc;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Response<TValue>
{
    public bool Success { get; set; }
    public List<string?> Message { get; set; }
    public TValue? Value { get; set; }
}