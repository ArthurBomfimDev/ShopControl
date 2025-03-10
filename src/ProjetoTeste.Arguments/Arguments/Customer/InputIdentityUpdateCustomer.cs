using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityUpdateCustomer(long id, InputUpdateCustomer? inputUpdate) : BaseInputIdentityUpdate<InputUpdateCustomer>(id, inputUpdate) { }