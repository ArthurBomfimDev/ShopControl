using ProjetoTeste.Context;

namespace ProjetoTeste.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}