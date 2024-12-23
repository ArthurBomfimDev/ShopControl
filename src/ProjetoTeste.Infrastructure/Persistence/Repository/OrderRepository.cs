using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
