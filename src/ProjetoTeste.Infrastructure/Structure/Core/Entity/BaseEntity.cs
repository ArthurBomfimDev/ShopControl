using System.ComponentModel.DataAnnotations;

namespace ProjetoEstagioAPI.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
