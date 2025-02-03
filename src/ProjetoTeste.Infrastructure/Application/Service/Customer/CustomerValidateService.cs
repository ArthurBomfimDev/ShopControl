using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerValidateService : ICustomerValidateService
{

    #region CPFValidate
    public bool CPFValidate(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = Regex.Replace(cpf, "[^0-9]", string.Empty); //Substitui todo q não é digito por uma string vazia Ex = 123.456.789-09

        if (cpf.Length != 11) return false;

        if (new string(cpf[0], cpf.Length) == cpf) return false; // Verifica se o cpf é composto pelo primeiro digito repetido

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores1[i]; // (cpf[i] - '0') -> jeito de converter um caracter em um digito numerico (substitui o valor unico) ex '0' = valor unicode 48 -> '0': = 48 - 48 = 0
        }

        int resto = soma % 11;
        int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto; // (operador ternário)  Verifica o primeiro digito verificador se é menor que dois, ser for é igual a 0, se for maior que dois a conta é (11 - resto)

        if (cpf[9] - '0' != primeiroDigitoVerificador) return false; //verificar se o o primeiro digito veriicador é igual o primeiro

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
    #endregion

    #region EmailValidate
    public bool EmailValidate(string email)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) return false;
        try
        {
            var validate = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    #endregion

    #region Create
    public async Task<BaseResponse<List<CustomerValidate>>> ValidateCreate(List<CustomerValidate> listCustomerValidate)
    {
        var response = new BaseResponse<List<CustomerValidate>>();

        _ = (from i in listCustomerValidate
             where i.InputCreateCustomer.Name.Length > 64 || string.IsNullOrEmpty(i.InputCreateCustomer.Name) || string.IsNullOrWhiteSpace(i.InputCreateCustomer.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateCustomer.Name.Length > 64 ? $"O cliente com o nome: '{i.InputCreateCustomer.Name}' não pode ser cadastrado, pois o nome excede o limite de 64 caracteres."
             : $"O cliente com o nome: '{i.InputCreateCustomer.Name}' não pode ser cadastrado, pois o nome está vazio")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where EmailValidate(i.InputCreateCustomer.Email) == false || i.InputCreateCustomer.Email.Length > 64
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateCustomer.Email.Length > 64 ? $"O cliente: '{i.InputCreateCustomer.Name}' com o E-mail: '{i.InputCreateCustomer.Email}' não pode ser cadastrado, pois o e-mail excede o limite de 64 caracteres."
             : $"O cliente: '{i.InputCreateCustomer.Name}' com o E-mail: '{i.InputCreateCustomer.Email}' não pode ser cadastrado, pois o e-mail não é válido.")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where CPFValidate(i.InputCreateCustomer.CPF) == false
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O cliente: '{i.InputCreateCustomer.Name}' com o CPF: '{i.InputCreateCustomer.CPF}' não pode ser cadastrado, pois o CPF é inválido.")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where !i.Invalid
             where (i.InputCreateCustomer.Phone.Length > 15) || (i.InputCreateCustomer.Phone.Length < 11)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O cliente: '{i.InputCreateCustomer.Name}' com o telefone: '{i.InputCreateCustomer.Phone}' não pode ser cadastrado, pois o número de telefone é inválido.")
             select i).ToList();

        var create = (from i in listCustomerValidate
                      where !i.Invalid
                      select i).ToList();

        if (!create.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = create;
        return response;
        #region ValidateUnic
        ////var cpfExistsList = (from i in inputCreateList
        ////                     where _customerRepository.CPFExists(i.CPF) == true
        ////                     select i).ToList();

        ////if (cpfExistsList.Count > 0)
        ////{
        ////    foreach (var customer in cpfExistsList)
        ////    {
        ////        response.Message.Add(new Notification { Message = $" >>> CPF: {customer.CPF} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        ////    }
        ////    inputCreateList = (inputCreateList.Except(cpfExistsList).ToList());
        ////}

        ////if (inputCreateList.Count == 0)
        ////{
        ////    response.Success = false;
        ////    return response;
        ////}
        ////var emailExistsList = (from i in inputCreateList
        ////                       where _customerRepository.EmailExists(i.Email) == true
        ////                       select i).ToList();

        ////if (emailExistsList.Count > 0)
        ////{
        ////    foreach (var customer in emailExistsList)
        ////    {
        ////        response.Message.Add(new Notification { Message = $" >>> Email: {customer.Email} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        ////    }
        ////    inputCreateList = (inputCreateList.Except(emailExistsList).ToList());
        ////}

        ////if (inputCreateList.Count == 0)
        ////{
        ////    response.Success = false;
        ////    return response;
        ////}
        ////var phoneExistsList = (from i in inputCreateList
        ////                       where _customerRepository.PhoneExists(i.Phone) == true
        ////                       select i).ToList();

        ////if (phoneExistsList.Count > 0)
        ////{
        ////    foreach (var customer in phoneExistsList)
        ////    {
        ////        response.Message.Add(new Notification { Message = $" >>> Phone: {customer.Phone} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        ////    }
        ////    inputCreateList = (inputCreateList.Except(phoneExistsList).ToList());
        ////}
        #endregion
    }
    #endregion

    #region Update
    public async Task<BaseResponse<List<CustomerValidate>>> ValidateUpdate(List<CustomerValidate> listCustomerValidate)
    {
        var response = new BaseResponse<List<CustomerValidate>>();

        _ = (from i in listCustomerValidate
             where i.RepeteId != 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O ID: '{i.InputIdentityUpdateCustomer.Id}' está duplicado. Não é possível concluir a operação.")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.OriginalDTO == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O cliente com o ID: '{i.InputIdentityUpdateCustomer.Id}' não existe. Atualização não permitida.")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name.Length > 64 || string.IsNullOrEmpty(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name.Length > 64 ? $"O cliente com Id: {i.InputIdentityUpdateCustomer.Id} o nome: '{i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}' não pode ser atualizado, pois o nome excede o limite de 64 caracteres."
             : $"O cliente com Id: {i.InputIdentityUpdateCustomer.Id} não pode ser atualizado, pois o nome está vazio")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where EmailValidate(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email) == false || i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.Length > 64
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(EmailValidate(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email) == false ? $"O clientecom Id: {i.InputIdentityUpdateCustomer.Id}' com o e-mail: '{i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email}' não pode ser atualizado, pois o e-mail é inválido."
             : $"O clientecom Id: {i.InputIdentityUpdateCustomer.Id}' com o e-mail: '{i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email}' não pode ser atualizado, pois o e-mail excede o limite de 64 caracteres.")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where CPFValidate(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF) == false
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O clientecom Id: {i.InputIdentityUpdateCustomer.Id} com o CPF: '{i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF}' não pode ser atualizado, pois o CPF é inválido.")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone.Length > 15 || i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone.Length < 11
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O clientecom Id: {i.InputIdentityUpdateCustomer.Id} com o telefone: '{i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone}' não pode ser atualizado, pois o número de telefone é inválido.")
             select i).ToList();

        var update = (from i in listCustomerValidate
                      where !i.Invalid
                      select i).ToList();

        if (!listCustomerValidate.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = update;
        return response;

        #region Unique
        //if (idList.Count != inputUpdateList.Count)
        //{
        //    response.Success = false;
        //    response.AddErrorMessage(" >>> ERRO - A Quantidade de Id's Digitados é Diferente da Quantdade de Marcas <<<");
        //    return response;
        //}

        //var notExists = (from i in idList
        //                 where _customerRepository.Exists(i) == false
        //                 select idList.IndexOf(i)).ToList();

        //if (notExists.Count > 0)
        //{
        //    for (int i = 0; i < notExists.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> O Cliente com id: {inputUpdateList[notExists[i]]} não existe <<<");
        //        idList.Remove(idList[notExists[i]]);
        //        inputUpdateList.Remove(inputUpdateList[notExists[i]]);
        //    }
        //}

        //if (inputUpdateList.Count == 0)
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var cpfExists = (from i in inputUpdateList
        //                 where CPFValidate(i.CPF) == false
        //                 select i).ToList();
        //if (cpfExists.Count > 0)
        //{
        //    foreach (var customer in cpfExists)
        //    {
        //        response.AddErrorMessage($" >>> Cliente: {customer.Name} com CPF: {customer.CPF} é inválido <<< ");
        //        var index = cpfExists.IndexOf(customer);
        //        idList.RemoveAt(index);
        //    }
        //    inputUpdateList = inputUpdateList.Except(cpfExists).ToList();
        //}

        //if (inputUpdateList.Count == 0) { response.Success = false; return response; }

        //var existingCustomer = await _customerRepository.GetListByListId(idList);

        //for (int i = 0; i < existingCustomer.Count; i++)
        //{
        //    existingCustomer[i].Name = inputUpdateList[i].Name;
        //    existingCustomer[i].CPF = inputUpdateList[i].CPF;
        //    existingCustomer[i].Email = inputUpdateList[i].Email;
        //    existingCustomer[i].Phone = inputUpdateList[i].Phone;
        //}

        //response.Content = existingCustomer;
        //return response;
        #endregion
    }
    #endregion

    #region Delete
    public async Task<BaseResponse<List<CustomerValidate>>> ValidateDelete(List<CustomerValidate> listCustomerValidate)
    {
        var response = new BaseResponse<List<CustomerValidate>>();

        _ = (from i in listCustomerValidate
             where i.RepeatedDelete != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Id: {i.InputIdentifyDeleteCustomer.Id} foi digitado repetidas vezes, não é possível deletar o cliente com esse Id")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.OriginalDTO == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Cliente com ID: {i.InputIdentifyDeleteCustomer.Id} é inválido. Verifique os dados.")
             select i).ToList();

        var delete = (from i in listCustomerValidate
                      where !i.Invalid
                      select i).ToList();

        if (!delete.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = delete;
        return response;
    }
    #endregion
}