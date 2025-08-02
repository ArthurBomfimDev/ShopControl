using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Product;

namespace ShopControl.Arguments.Arguments;

public class InputIdentityUpdateProduct(long id, InputUpdateProduct? inputUpdate) : BaseInputIdentityUpdate<InputUpdateProduct>(id, inputUpdate) { }