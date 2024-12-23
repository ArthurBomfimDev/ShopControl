using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Context;
using ProjetoTeste.Infrastructure.Default;
using ProjetoTeste.Models;

namespace ProjetoTeste.Infrastructure.Orders
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
