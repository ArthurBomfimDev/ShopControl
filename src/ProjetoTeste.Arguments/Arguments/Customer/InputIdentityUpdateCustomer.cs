using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityUpdateCustomer : BaseInputIdentityUpdate<InputIdentityUpdateCustomer>
{
    public long Id { get; private set; }
    public InputUpdateCustomer InputUpdateCustomer { get; private set; }

    public InputIdentityUpdateCustomer()
    {
        
    }

    [JsonConstructor]
    public InputIdentityUpdateCustomer(long id, InputUpdateCustomer inputUpdateCustomer)
    {
        Id = id;
        InputUpdateCustomer = inputUpdateCustomer;
    }
}
