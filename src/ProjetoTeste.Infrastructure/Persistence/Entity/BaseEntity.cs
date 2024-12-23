using System.ComponentModel.DataAnnotations;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;
public class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
