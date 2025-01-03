using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetProductOrders()
        {
            var get = await _context.Set<Order>().Include(o => o.ProductOrders).ToListAsync();
            return get;
        }

        public async Task<List<Order>> GetProductOrdersId(long id)
        {
            var get = await _context.Set<Order>().Include(o => o.ProductOrders)
                .Where(o => o.Id == id).ToListAsync();
            return get;
        }

        public async Task<List<Order>> GetProductOrdersLINQ()
        {
            var get = await _context.Set<Order>()
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.ProductId).ToListAsync();
            return get;
        }
    }
}