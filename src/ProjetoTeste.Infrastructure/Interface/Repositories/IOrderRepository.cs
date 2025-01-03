using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetProductOrders();
        Task<List<Order>> GetProductOrdersId(long id);

    }
}
