using ProjetoTeste.Arguments.Arguments.Customer;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class CustomerDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public virtual List<OrderDTO?> ListOrder { get; set; }

    public CustomerDTO() { }

    [JsonConstructor]
    public CustomerDTO(long id, string name, string cPF, string email, string phone, List<OrderDTO?> listOrder = null)
    {
        Id = id;
        Name = name;
        CPF = cPF;
        Email = email;
        Phone = phone;
        ListOrder = listOrder;
    }

    #region Implicit Conversor
    public static implicit operator OutputCustomer(CustomerDTO customerDTO)
    {
        return customerDTO != null ? new OutputCustomer
            (
            customerDTO.Id,
            customerDTO.Name,
            customerDTO.CPF,
            customerDTO.Email,
            customerDTO.Phone
            ) : null;
    }
    #endregion
}