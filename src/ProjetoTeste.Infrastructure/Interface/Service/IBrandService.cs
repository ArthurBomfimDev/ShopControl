using ProjetoTeste.Arguments.Arguments.Brands;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    internal interface IBrandService
    {
        Task<Response<List<OutputBrand>>> GetAll();
        Task<Response<OutputBrand>> Get(long id);
        Task<Response<OutputBrand>> Create(InputCreateBrand input);
        Task<Response<bool>> Update(long id, InputUpdateBrand brand);
        Task<Response<bool>> Delete(long id);
    }
}
