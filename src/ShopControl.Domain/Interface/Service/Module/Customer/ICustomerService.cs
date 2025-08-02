using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Customer;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service.Base;

namespace ShopControl.Domain.Interface.Service;

public interface ICustomerService : IBaseService<CustomerDTO, InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, InputIdentifyViewCustomer, OutputCustomer> { }