using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    public class OutputCustomer : BaseOutput<OutputCustomer>
    {
        public long id { get; private set; }
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
            this.id = id;
            Name = name;
            CPF = cPF;
            Email = email;
            Phone = phone;
        }
    }
}
