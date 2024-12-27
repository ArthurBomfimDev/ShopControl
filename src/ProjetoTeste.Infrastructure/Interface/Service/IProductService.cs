using ProjetoTeste.Arguments.Arguments.Products;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    internal interface IProductService
    {
        Task<Response<List<OutputProduct>>> GetAll();
        Task<Response<OutputProduct>> Get(long id);
        Task<Response<OutputProduct>> Delete(long id);
        Task<Response<OutputProduct>> Create(InputCreateProduct product);
        Task<Response<OutputProduct>> Update(long id, InputUpdateProduct product);
    }
}
