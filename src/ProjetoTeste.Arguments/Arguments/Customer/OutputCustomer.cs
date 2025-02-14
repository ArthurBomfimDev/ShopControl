using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    public class OutputCustomer : BaseOutput<OutputCustomer>
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public OutputCustomer()
        {

        }

        [JsonConstructor]
        public OutputCustomer(long id, string name, string cPF, string email, string phone)
        {
            Id = id;
            Name = name;
            CPF = cPF;
            Email = email;
            Phone = phone;
        }
    }
}
