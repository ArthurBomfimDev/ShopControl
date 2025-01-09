using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<List<OutputCustomer>>> GetAll()
    {
        var clientList = await _customerRepository.GetAllAsync();
        return new BaseResponse<List<OutputCustomer>>() { Success = true, Content = (from i in clientList select i.ToOutputCustomer()).ToList() };
    }

    public async Task<BaseResponse<OutputCustomer>> Get(List<long> ids)
    {
        var customer = await _customerRepository.Get(ids);
        return new BaseResponse<OutputCustomer> { Success = true/*, Content = customer.ToOutputCustomer() */};
    }

    //public async Task<BaseResponse<OutputCustomer>> Create(InputCreateCustomer customer)
    //{
    //    var response = new BaseResponse<OutputCustomer>();
    //    if (customer is null)
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> Dados Inseridos Inválidos <<< ");
    //    }
    //    if (await _customerRepository.CPFExists(customer.CPF))
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> CPF Já Cadastrado <<< ");
    //    }
    //    if (await _customerRepository.EmailExists(customer.Email))
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> Email Já Cadastrado <<< ");
    //    }
    //    if (await _customerRepository.PhoneExists(customer.Phone))
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> Número de telefone Já Cadastrado <<< ");
    //    }
    //    if (!CpfValidate(customer.CPF))
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> Digite um CPF válido <<< ");
    //    }
    //    if (!(customer.Phone.Length == 11))
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> Digite um número de Telefone válido <<< ");
    //    }
    //    if (!response.Success)
    //    {
    //        return response;
    //    }
    //    var newCustomer = customer.ToCustomer();
    //    var createCustomer = await _customerRepository.Create(newCustomer);
    //    return new BaseResponse<OutputCustomer> { Success = true, Content = createCustomer.ToOutputCustomer() };
    //}

    //public async Task<BaseResponse<bool>> Update(long id, InputUpdateCustomer customer)
    //{
    //    var customerExists = await _customerRepository.Get(id);
    //    var response = new BaseResponse<bool>();
    //    if (customerExists == null)
    //    {
    //        return new BaseResponse<bool>() { Success = false, Message = { " >>> Cliente com o Id digitado NÃO encontrado <<<" } };
    //    }
    //    if (customer is null)
    //    {
    //        return new BaseResponse<bool>() { Success = false, Message = { " >>> Dados Inválidos <<<" } };

    //    }
    //    if (!string.Equals(customer.CPF, customerExists.CPF, StringComparison.OrdinalIgnoreCase))
    //    {
    //        if (await _customerRepository.CPFExists(customer.CPF))
    //        {
    //            response.Success = false;
    //            response.Message.Add(" >>> CPF Já Cadastrado <<< ");
    //        }
    //    }
    //    if (!string.Equals(customer.Email, customerExists.Email, StringComparison.OrdinalIgnoreCase))
    //    {
    //        if (await _customerRepository.EmailExists(customer.Email))
    //        {
    //            response.Success = false;
    //            response.Message.Add(" >>> Email Já Cadastrado <<< ");
    //        }
    //    }
    //    if (!string.Equals(customer.Phone, customerExists.Phone, StringComparison.OrdinalIgnoreCase))
    //    {
    //        if (await _customerRepository.PhoneExists(customer.Phone))
    //        {
    //            response.Success = false;
    //            response.Message.Add(" >>> Número de Telefone Já Cadastrado <<< ");
    //        }
    //    }
    //    if (!CpfValidate(customer.CPF))
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> Digite um CPF válido <<< ");
    //    }
    //    if (!(customer.Phone.Length == 11))
    //    {
    //        response.Success = false;
    //        response.Message.Add(" >>> Digite um número de Telefone válido <<< ");
    //    }
    //    if (!response.Success)
    //    {
    //        return response;
    //    }
    //    customerExists.Email = customer.Email;
    //    customerExists.Phone = customer.Phone;
    //    customerExists.CPF = customer.CPF;
    //    customerExists.Name = customer.Name;
    //    _customerRepository.Update(customerExists);
    //    return new BaseResponse<bool> { Success = true, Message = { "Cliente Atualizado com SUCESSO" } };
    //}

    public async Task<BaseResponse<bool>> Delete(List<long> id)
    {
        var customerExists = await _customerRepository.Get(id);
        if (customerExists == null)
        {
            return new BaseResponse<bool>() { Success = false, Message = new List<Notification> { new Notification { Message = " >>> Cliente com o Id digitado NÃO encontrado <<<", Type = EnumNotificationType.Error } } };
        }
        var delete = await _customerRepository.Get(id);
        await _customerRepository.Delete(delete);
        return new BaseResponse<bool>() { Success = true, Message = new List<Notification> { new Notification { Message = " >>> Cliente deletado com SUCESSO <<<", Type = EnumNotificationType.Success } } };
    }

    Task<BaseResponse<List<OutputCustomer>>> ICustomerService.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<OutputCustomer>> ICustomerService.Get(long id)
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<bool>> ICustomerService.Delete(long id)
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<OutputCustomer>> ICustomerService.Create(InputCreateCustomer client)
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<bool>> ICustomerService.Update(long id, InputUpdateCustomer client)
    {
        throw new NotImplementedException();
    }
}
