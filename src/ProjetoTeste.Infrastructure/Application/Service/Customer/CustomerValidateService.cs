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

    public async Task<BaseResponse<List<InputCreateCustomer>>> ValidateCreate(List<InputCreateCustomer> inputCreateList)
    {
        var response = new BaseResponse<List<InputCreateCustomer>>();

        var cpfValidateList = (from i in inputCreateList
                               where CpfValidate(i.CPF) == false
                               select i).ToList();

        if (cpfValidateList.Count > 0)
        {
            foreach (var customer in cpfValidateList)
            {
                response.Message.Add(new Notification { Message = $" >>> Cliente: {customer.Name} com CPF: {customer.CPF} é inválido <<< ", Type = EnumNotificationType.Error });
            }
            inputCreateList = (inputCreateList.Except(cpfValidateList).ToList());
        }

        if (inputCreateList.Count == 0)
        {
            response.Success = false;
            return response;
        }

        #region ValidateUnic
        //var cpfExistsList = (from i in inputCreateList
        //                     where _customerRepository.CPFExists(i.CPF) == true
        //                     select i).ToList();

        //if (cpfExistsList.Count > 0)
        //{
        //    foreach (var customer in cpfExistsList)
        //    {
        //        response.Message.Add(new Notification { Message = $" >>> CPF: {customer.CPF} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        //    }
        //    inputCreateList = (inputCreateList.Except(cpfExistsList).ToList());
        //}

        //if (inputCreateList.Count == 0)
        //{
        //    response.Success = false;
        //    return response;
        //}
        //var emailExistsList = (from i in inputCreateList
        //                       where _customerRepository.EmailExists(i.Email) == true
        //                       select i).ToList();

        //if (emailExistsList.Count > 0)
        //{
        //    foreach (var customer in emailExistsList)
        //    {
        //        response.Message.Add(new Notification { Message = $" >>> Email: {customer.Email} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        //    }
        //    inputCreateList = (inputCreateList.Except(emailExistsList).ToList());
        //}

        //if (inputCreateList.Count == 0)
        //{
        //    response.Success = false;
        //    return response;
        //}
        //var phoneExistsList = (from i in inputCreateList
        //                       where _customerRepository.PhoneExists(i.Phone) == true
        //                       select i).ToList();

        //if (phoneExistsList.Count > 0)
        //{
        //    foreach (var customer in phoneExistsList)
        //    {
        //        response.Message.Add(new Notification { Message = $" >>> Phone: {customer.Phone} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        //    }
        //    inputCreateList = (inputCreateList.Except(phoneExistsList).ToList());
        //}
        #endregion

        if (inputCreateList.Count == 0)
        {
            response.Success = false;
            return response;
        }
        response.Content = inputCreateList;
        return response;
    }

    public async Task<BaseResponse<List<Customer>>> ValidateUpdate(List<long> idList, List<InputUpdateCustomer> inputUpdateList)
    {
        var response = new BaseResponse<List<Customer>>();
        if (idList.Count != inputUpdateList.Count)
        {
            response.Success = false;
            response.Message.Add(new Notification { Message = " >>> ERRO - A Quantidade de Id's Digitados é Diferente da Quantdade de Marcas <<<", Type = EnumNotificationType.Error });
            return response;
        }

        var notExists = (from i in idList
                         where _customerRepository.Exists(i) == false
                         select idList.IndexOf(i)).ToList();

        if(notExists.Count > 0)
        {
            for(int i = 0; i < notExists.Count; i++)
            {
                response.Message.Add(new Notification {  Message = $" >>> O Cliente com id: {inputUpdateList[notExists[i]]} não existe <<<", Type = EnumNotificationType.Error });
                idList.Remove(idList[notExists[i]]);
                inputUpdateList.Remove(inputUpdateList[notExists[i]]);
            }
        }

        if(inputUpdateList.Count == 0)
        {
            response.Success = false;
            return response;
        }

        var cpfExists = (from i in inputUpdateList
                        where CpfValidate(i.CPF) == false
                        select i).ToList();
        if(cpfExists.Count > 0)
        {
            foreach (var customer in cpfExists)
            {
                response.Message.Add(new Notification { Message = $" >>> Cliente: {customer.Name} com CPF: {customer.CPF} é inválido <<< ", Type = EnumNotificationType.Error });
                var index = cpfExists.IndexOf(customer);
                idList.RemoveAt(index);
            }
            inputUpdateList = inputUpdateList.Except(cpfExists).ToList();
        }

        if (inputUpdateList.Count == 0) { response.Success = false; return response; }

        var existingCustomer = await _customerRepository.Get(idList);

        for(int i = 0; i < existingCustomer.Count; i++)
        {
            existingCustomer[i].Name = inputUpdateList[i].Name;
            existingCustomer[i].CPF = inputUpdateList[i].CPF;
            existingCustomer[i].Email = inputUpdateList[i].Email;
            existingCustomer[i].Phone = inputUpdateList[i].Phone;
        }

        response.Content = existingCustomer;
        return response;
    }

    public async Task<BaseResponse<List<Customer>>> ValidateDelete(List<long> idList)
    {
        var response = new BaseResponse<List<Customer>>();
        var idExists = (from i in idList
                       where _customerRepository.Exists(i) == false
                       select i).ToList();

        if (idExists.Count > 0)
        {
            foreach (var id in idExists)
            {
                response.Message.Add(new Notification { Message = $" >>> O Cliente com Id: {id} não Existe <<<", Type = EnumNotificationType.Error });
            }
            idList = idList.Except(idExists).ToList();
        }

        if(idList.Count() == 0)
        {
            response.Success = false;
            return response;
        }

        var customerList = await _customerRepository.Get(idList);
        response.Content = customerList;
        return response;
    }
}