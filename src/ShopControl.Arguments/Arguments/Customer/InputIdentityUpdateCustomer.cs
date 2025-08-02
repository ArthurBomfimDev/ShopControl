using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Customer;

namespace ShopControl.Arguments.Arguments;

public class InputIdentityUpdateCustomer(long id, InputUpdateCustomer? inputUpdate) : BaseInputIdentityUpdate<InputUpdateCustomer>(id, inputUpdate) { }