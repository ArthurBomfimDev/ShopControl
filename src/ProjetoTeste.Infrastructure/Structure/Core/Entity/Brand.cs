using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTeste.Models
{
    [Table("Brand")]
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public List<Product>? Products { get; set; }

        public Brand()
        { }

        public Brand(string name, string code, string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }
        public Brand(long Id, string name, string code, string description)
        {
            Id = Id;
            Name = name;
            Code = code;
            Description = description;
        }
    }
}
