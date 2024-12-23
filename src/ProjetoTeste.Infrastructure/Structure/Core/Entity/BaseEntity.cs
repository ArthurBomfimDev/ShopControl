using System.ComponentModel.DataAnnotations;

namespace ProjetoTeste.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
