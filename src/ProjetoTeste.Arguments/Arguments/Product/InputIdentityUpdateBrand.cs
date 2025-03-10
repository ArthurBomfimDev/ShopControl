using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityUpdateProduct(long id, InputUpdateProduct? inputUpdate) : BaseInputIdentityUpdate<InputUpdateProduct>(id, inputUpdate) { }