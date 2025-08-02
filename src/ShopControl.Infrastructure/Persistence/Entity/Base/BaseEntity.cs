using System.ComponentModel.DataAnnotations;

namespace ShopControl.Infrastructure.Persistence.Entity.Base;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
