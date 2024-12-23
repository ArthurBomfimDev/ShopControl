using ProjetoEstagioAPI.Context;
using ProjetoEstagioAPI.Infrastructure.Brands;
using ProjetoEstagioAPI.Infrastructure.Clients;
using ProjetoEstagioAPI.Infrastructure.Orders;
using ProjetoEstagioAPI.Infrastructure.Products;

namespace ProjetoEstagioAPI.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}