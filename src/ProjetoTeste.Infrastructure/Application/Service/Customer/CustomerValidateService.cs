using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System.Text.RegularExpressions;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerValidateService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerValidateService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
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

    public async Task<BaseResponse<List<Customer>>> ValidateCreate(List<InputCreateCustomer> inputCreateCustomerList)
    {
        var response = new BaseResponse<List<Customer>>();

        var cpfExistList = (from i in inputCreateCustomerList
                            where CpfValidate(i.CPF) == false
                            select i).ToList();

        if (cpfExistList.Count > 0)
        {
            foreach (var customer in cpfExistList)
            {
                response.Message.Add(new Notification { Message = $" >>> CPF: {customer.CPF} do Cliente: {customer.Name} é inválido <<< ", Type = EnumNotificationType.Error });
            }
        }

        if (await (inputCreateCustomerList.cpf))
        {
            response.success = false;
            response.message.add(" >>> cpf já cadastrado <<< ");
        }
        if (await _inputCreateCustomerListrepository.emailexists(inputCreateCustomerList.email))
        {
            response.success = false;
            response.message.add(" >>> email já cadastrado <<< ");
        }
        if (await _inputCreateCustomerListrepository.phoneexists(inputCreateCustomerList.phone))
        {
            response.success = false;
            response.message.add(" >>> número de telefone já cadastrado <<< ");
        }
        if (!cpfvalidate(inputCreateCustomerList.cpf))
        {
            response.success = false;
            response.message.add(" >>> digite um cpf válido <<< ");
        }
        if (!(inputCreateCustomerList.phone.length == 11))
        {
            response.success = false;
            response.message.add(" >>> digite um número de telefone válido <<< ");
        }
        if (!response.success)
        {
            return response;
        }
        var newinputCreateCustomerList = inputCreateCustomerList.toinputCreateCustomerList();
        var createinputCreateCustomerList = await _inputCreateCustomerListrepository.create(newinputCreateCustomerList);
        return new baseresponse<outputinputCreateCustomerList> { success = true, content = createinputCreateCustomerList.tooutputinputCreateCustomerList() };
    }
}