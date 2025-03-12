using System.ComponentModel.DataAnnotations;

namespace ProjetoTeste.Infrastructure.Persistence.Entity.Base;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
