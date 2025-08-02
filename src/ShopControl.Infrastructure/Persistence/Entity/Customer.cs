using ShopControl.Domain.DTO;
using ShopControl.Infrastructure.Persistence.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopControl.Infrastructure.Persistence.Entity;

[Table("cliente")]
public class Customer : BaseEntity
{
    [Required]
    [Column("nome")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]   
    [Column("CPF")]
    [MaxLength(11)]
    public string CPF { get; set; }

    [Required]
    [Column("email")]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }

    [Required]
    [Column("telefone")]
    [Phone]
    [MaxLength(15)]
    public string Phone { get; set; }

    public virtual List<Order>? ListOrder { get; set; }

    public Customer()
    { }

    public Customer(string name, string cPF, string email, string phone)
    {
        Name = name;
        CPF = cPF;
        Email = email;
        Phone = phone;
    }

    #region Implicit Conversor
    public static implicit operator Customer(CustomerDTO customerDTO)
    {
        return customerDTO != null ? new Customer
        {
            Id = customerDTO.Id,
            Name = customerDTO.Name,
            CPF = customerDTO.CPF,
            Email = customerDTO.Email,
            Phone = customerDTO.Phone,
            ListOrder = customerDTO.ListOrder != null ? customerDTO.ListOrder.Select(i => (Order)i).ToList() : null
        } : null;
    }
    public static implicit operator CustomerDTO(Customer customer)
    {
        return customer != null ? new CustomerDTO
        (
            customer.Id,
            customer.Name,
            customer.CPF,
            customer.Email,
            customer.Phone,
            customer.ListOrder != null ? customer.ListOrder.Select(i => (OrderDTO)i).ToList() : null
        ) : null;
    }
    #endregion
}