using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerValidateService _validateDeleteRepository;

    public CustomerService(ICustomerRepository customerRepository, CustomerValidateService validateDeleteRepository)
    {
        _customerRepository = customerRepository;
        _validateDeleteRepository = validateDeleteRepository;
    }

    public async Task<List<OutputCustomer>> GetAll()
    {
        var customerList = await _customerRepository.GetAll();
        return (from i in customerList select i.ToOutputCustomer()).ToList();
    }

    public async Task<OutputCustomer> Get(long id)
    {
        var customer = await _customerRepository.Get(id);
        return customer.ToOutputCustomer();
    }

    public async Task<List<OutputCustomer>> GetListByListId(List<long> idList)
    {
        var customerList = await _customerRepository.GetListByListId(idList);
        return (from i in customerList select i.ToOutputCustomer()).ToList();
    }

    public async Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> client)
    {
        var validateCreate = await _validateDeleteRepository.ValidateCreate(client);
        var response = new BaseResponse<List<OutputCustomer>>() { Message = validateCreate.Message };
        if (!validateCreate.Success)
        {
            response.Success = false;
            return response;
        }

        var customerList = (from i in validateCreate.Content
                            select new Customer(i.Name, i.CPF, i.Email, i.Phone, default)).ToList();

        var createcustomerList = await _customerRepository.Create(customerList);

        response.Content = (from i in createcustomerList
                            select new OutputCustomer(i.Id, i.Name, i.CPF, i.Email, i.Phone)).ToList();
        return response;
    }

    public async Task<BaseResponse<bool>> Update(List<long> idList, List<InputUpdateCustomer> client)
    {
        var validateUpdate = await _validateDeleteRepository.ValidateUpdate(idList, client);
        var response = new BaseResponse<bool>() { Message = validateUpdate.Message };

        if (!validateUpdate.Success)
        {
            response.Success = false;
            return response;
        }

        var customerUpdateList = await _customerRepository.Update(validateUpdate.Content);

        foreach (var customer in validateUpdate.Content)
        {
            response.AddSuccessMessage($" >>> Cliente: {customer.Name} com Id: {customer.Id} foi atualizado com SUCESSO <<<");
        }
        return response;
    }

    public async Task<BaseResponse<bool>> Delete(List<long> idList)
    {
        var validateDelete = await _validateDeleteRepository.ValidateDelete(idList);
        var response = new BaseResponse<bool>() { Message = validateDelete.Message };

        if (!validateDelete.Success)
        {
            response.Success = false;
            return response;
        }

        await _customerRepository.Delete(validateDelete.Content);
        foreach (var customer in validateDelete.Content)
        {
            response.AddSuccessMessage($" >>> Cliente: {customer.Name} com Id: {customer.Id} foi deletado com SUCESSO <<<");
        }
        return response;
    }
}