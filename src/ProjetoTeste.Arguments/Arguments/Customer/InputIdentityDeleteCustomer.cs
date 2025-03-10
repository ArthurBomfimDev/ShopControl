using ProjetoTeste.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityDeleteCustomer(long id) : BaseInputIdentityDelete<InputIdentityDeleteCustomer>(id) { }