using ShopControl.Api.Controllers.Base;
using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Customer;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Service;
using ShopControl.Infrastructure.Interface.UnitOfWork;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Api.Controllers;

public class CustomerController(ICustomerService customerService, IUnitOfWork unitOfWork) : BaseController<ICustomerService, CustomerDTO, Customer, InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, InputIdentifyViewCustomer, OutputCustomer>(unitOfWork, customerService) { }
