using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Client;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Response<TValue>
{
    public bool Success { get; set; } = true;
    public List<string?> Message { get; set; } = new List<string?>();
    public TValue? Value { get; set; }

    public static implicit operator Response<TValue>(ActionResult<OutputClient> v)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Response<TValue>(ActionResult v)
    {
        throw new NotImplementedException();
    }
}