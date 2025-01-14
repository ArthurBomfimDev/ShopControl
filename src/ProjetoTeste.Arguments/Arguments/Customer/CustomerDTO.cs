namespace ProjetoTeste.Arguments.Arguments;

public class CustomerDTO
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<OrderDTO>? ListOrder { get; set; }
}
