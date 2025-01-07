using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System.Text.RegularExpressions;

namespace ProjetoTeste.Infrastructure.Application.Service;

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
        return new BaseResponse<List<OutputCustomer>>() { Success = true, Value = (from i in clientList select i.ToOutputCustomer()).ToList() };
    }

    public async Task<BaseResponse<OutputCustomer>> Get(long id)
    {
        var customer = await _customerRepository.Get(id);
        return new BaseResponse<OutputCustomer> { Success = true, Value = customer.ToOutputCustomer() };
    }

    public async Task<BaseResponse<OutputCustomer>> Create(InputCreateCustomer customer)
    {
        var response = new BaseResponse<OutputCustomer>();
        if (customer is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Dados Inseridos Inválidos <<< ");
        }
        if (await _customerRepository.CPFExists(customer.CPF))
        {
            response.Success = false;
            response.Message.Add(" >>> CPF Já Cadastrado <<< ");
        }
        if (await _customerRepository.EmailExists(customer.Email))
        {
            response.Success = false;
            response.Message.Add(" >>> Email Já Cadastrado <<< ");
        }
        if (await _customerRepository.PhoneExists(customer.Phone))
        {
            response.Success = false;
            response.Message.Add(" >>> Número de telefone Já Cadastrado <<< ");
        }
        if (!CpfValidate(customer.CPF))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um CPF válido <<< ");
        }
        if (!(customer.Phone.Length == 11))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um número de Telefone válido <<< ");
        }
        if (!response.Success)
        {
            return response;
        }
        var newCustomer = customer.ToCustomer();
        var createCustomer = await _customerRepository.Create(newCustomer);
        return new BaseResponse<OutputCustomer> { Success = true, Value = createCustomer.ToOutputCustomer() };
    }

    public async Task<BaseResponse<bool>> Update(long id, InputUpdateCustomer customer)
    {
        var customerExists = await _customerRepository.Get(id);
        var response = new BaseResponse<bool>();
        if (customerExists == null)
        {
            return new BaseResponse<bool>() { Success = false, Message = { " >>> Cliente com o Id digitado NÃO encontrado <<<" } };
        }
        if (customer is null)
        {
            return new BaseResponse<bool>() { Success = false, Message = { " >>> Dados Inválidos <<<" } };

        }
        if (!string.Equals(customer.CPF, customerExists.CPF, StringComparison.OrdinalIgnoreCase))
        {
            if (await _customerRepository.CPFExists(customer.CPF))
            {
                response.Success = false;
                response.Message.Add(" >>> CPF Já Cadastrado <<< ");
            }
        }
        if (!string.Equals(customer.Email, customerExists.Email, StringComparison.OrdinalIgnoreCase))
        {
            if (await _customerRepository.EmailExists(customer.Email))
            {
                response.Success = false;
                response.Message.Add(" >>> Email Já Cadastrado <<< ");
            }
        }
        if (!string.Equals(customer.Phone, customerExists.Phone, StringComparison.OrdinalIgnoreCase))
        {
            if (await _customerRepository.PhoneExists(customer.Phone))
            {
                response.Success = false;
                response.Message.Add(" >>> Número de Telefone Já Cadastrado <<< ");
            }
        }
        if (!CpfValidate(customer.CPF))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um CPF válido <<< ");
        }
        if (!(customer.Phone.Length == 11))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um número de Telefone válido <<< ");
        }
        if (!response.Success)
        {
            return response;
        }
        customerExists.Email = customer.Email;
        customerExists.Phone = customer.Phone;
        customerExists.CPF = customer.CPF;
        customerExists.Name = customer.Name;
        _customerRepository.Update(customerExists);
        return new BaseResponse<bool> { Success = true, Message = { "Cliente Atualizado com SUCESSO" } };
    }

    public async Task<BaseResponse<bool>> Delete(long id)
    {
        var customerExists = await _customerRepository.Get(id);
        if (customerExists == null)
        {
            return new BaseResponse<bool>() { Success = false, Message = { " >>> Cliente com o Id digitado NÃO encontrado <<<" } };
        }
        await _customerRepository.Delete(id);
        return new BaseResponse<bool>() { Success = true, Message = { " >>> Cliente deletado com SUCESSO <<<" } };
    }

    public bool CpfValidate(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = Regex.Replace(cpf, "[^0-9]", string.Empty);

        if (cpf.Length != 11) return false;

        if (new string(cpf[0], cpf.Length) == cpf) return false;

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores1[i];
        }

        int resto = soma % 11;
        int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

        if (cpf[9] - '0' != primeiroDigitoVerificador) return false;

        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores2[i];
        }

        resto = soma % 11;
        int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

        if (cpf[10] - '0' != segundoDigitoVerificador) return false;

        return true;
    }

}
