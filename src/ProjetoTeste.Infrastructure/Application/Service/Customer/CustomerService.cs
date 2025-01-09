using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerValidateService _customerValidateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, CustomerValidateService customerValidateRepository)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _customerValidateRepository = customerValidateRepository;
    }

    public async Task<BaseResponse<List<OutputCustomer>>> GetAll()
    {
        var customerList = await _customerRepository.GetAllAsync();
        return new BaseResponse<List<OutputCustomer>>() { Success = true, Content = (from i in customerList select i.ToOutputCustomer()).ToList() };
    }

    public async Task<BaseResponse<List<OutputCustomer>>> Get(List<long> ids)
    {
        var customerList = await _customerRepository.Get(ids);
        return new BaseResponse<List<OutputCustomer>> { Success = true, Content = (from i in customerList select i.ToOutputCustomer()).ToList() };
    }

    public async Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> client)
    {
        var inputCreateValidate = await _customerValidateRepository.ValidateCreate(client);
        if (!inputCreateValidate.Success) return new BaseResponse<List<OutputCustomer>>() { Success = false, Message = inputCreateValidate.Message };

        var customerList = (from i in inputCreateValidate.Content
                           select new Customer(i.Name, i.CPF, i.Email, i.Phone, default)).ToList();

        var createcustomerList = await _customerRepository.Create(customerList);

        return new BaseResponse<List<OutputCustomer>>() { Success = true, Message = inputCreateValidate.Message, Content = (from i in customerList
                                                                                                              select new OutputCustomer(i.Id,i.Name, i.CPF, i.Email, i.Phone)).ToList()};
    }

    public async Task<BaseResponse<bool>> Update(List<long> idList, List<InputUpdateCustomer> client)
    {
        var validateUpdate = await _customerValidateRepository.ValidateUpdate(idList, client);
        if (!validateUpdate.Success)
        {
            return new BaseResponse<bool>() { Success = false, Message = validateUpdate.Message };
        }

        var customerUpdateList = await _customerRepository.Update(validateUpdate.Content);
        
        foreach(var customer in validateUpdate.Content)
        {
            validateUpdate.Message.Add(new Notification { Message = $" >>> Cliente: {customer.Name} com Id: {customer.Id} foi atualizado com SUCESSO <<<", Type = EnumNotificationType.Success });
        }
        return new BaseResponse<bool>() { Success = true, Message = validateUpdate.Message };
    }

    public async Task<BaseResponse<bool>> Delete(List<long> idList)
    {
        var customerValidate = await _customerValidateRepository.ValidateDelete(idList);
        if (!customerValidate.Success)
        {
            return new BaseResponse<bool>() { Success = false, Message = customerValidate.Message };
        }
        await _customerRepository.Delete(customerValidate.Content);
        foreach( var customer in customerValidate.Content)
        {
            customerValidate.Message.Add(new Notification { Message = $" >>> Cliente: {customer.Name} com Id: {customer.Id} foi deletado com SUCESSO <<<", Type = EnumNotificationType.Success });
        }
        return new BaseResponse<bool>() { Success = true, Message = customerValidate.Message };
    }
}