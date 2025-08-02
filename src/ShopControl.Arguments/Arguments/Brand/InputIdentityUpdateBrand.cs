using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Brand;

namespace ShopControl.Arguments.Arguments;

public class InputIdentityUpdateBrand(long id, InputUpdateBrand? inputUpdate) : BaseInputIdentityUpdate<InputUpdateBrand>(id, inputUpdate) { }