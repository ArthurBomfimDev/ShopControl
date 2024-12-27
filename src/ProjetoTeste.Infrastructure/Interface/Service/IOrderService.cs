using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste.Infrastructure.Interface.Service;

internal interface IOrderService
{
    Task<Response<List<OutputOrder>>> GetAll();
    Task<Response<Order>> Get(long id);
    Task<Response<OutputOrder>> Delete(long id);
    Task<Response<OutputOrder>> Create(InputCreateOrder input);
    Task<Response<Order>> Total();
}
