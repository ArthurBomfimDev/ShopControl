using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityUpdateBrand(long id, InputUpdateBrand? inputUpdate) : BaseInputIdentityUpdate<InputUpdateBrand>(id, inputUpdate) { }