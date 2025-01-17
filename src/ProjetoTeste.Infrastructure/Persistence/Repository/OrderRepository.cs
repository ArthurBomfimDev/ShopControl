using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Order;
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
            var get = await _dbSet.Include(o => o.ListProductOrder).ToListAsync();
            return get;
        }
        public async Task<List<Order>> GetProductOrdersId(long id)
        {
            var get = await _dbSet.Include(o => o.ListProductOrder)
                .Where(o => o.Id == id).ToListAsync();
            return get;
        }
        public Task<List<Order>> GetProductOrdersByListId(List<long> listId)
        {
            return _dbSet.Include(i => i.ListProductOrder).Where(j => listId.Contains(j.Id)).ToListAsync();
        }

        public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
        {
            return (from i in _dbSet
                    from j in i.ListProductOrder
                    group j by j.Product into g
                    orderby g.Sum(k => k.Quantity) descending
                    select new
                    {
                        Product = g.Key,
                        TotalSaleQuantity = g.Sum(i => i.Quantity),
                        TotalSaleValue = g.Sum(i => i.SubTotal)
                    }).Select(k => new OutputMaxSaleValueProduct(k.Product.Id, k.Product.Name, k.Product.Code, k.Product.Description, k.TotalSaleValue, k.Product.BrandId, k.TotalSaleQuantity)).ToList();
        }

        public async Task<OutputMaxSaleValueProduct?> BestSellerProduct()
        {
            return (from i in _dbSet
                    from j in i.ListProductOrder
                    group j by j.Product into g
                    orderby g.Sum(k => k.Quantity) descending
                    select new
                    {
                        Product = g.Key,
                        TotalSaleQuantity = g.Sum(i => i.Quantity),
                        TotalSaleValue = g.Sum(i => i.SubTotal)
                    }).Select(k => new OutputMaxSaleValueProduct(k.Product.Id, k.Product.Name, k.Product.Code, k.Product.Description, k.TotalSaleValue, k.Product.BrandId, k.TotalSaleQuantity)).FirstOrDefault();
        }

        public async Task<OutputMaxSaleValueProduct?> LeastSoldProduct()
        {
            return (from i in _dbSet
                    from j in i.ListProductOrder
                    group j by j.Product into g
                    orderby g.Sum(k => k.Quantity) ascending
                    select new
                    {
                        Product = g.Key,
                        TotalSaleQuantity = g.Sum(i => i.Quantity),
                        TotalSaleValue = g.Sum(i => i.SubTotal)
                    }).Select(k => new OutputMaxSaleValueProduct(k.Product.Id, k.Product.Name, k.Product.Code, k.Product.Description, k.TotalSaleValue, k.Product.BrandId, k.TotalSaleQuantity)).FirstOrDefault();
        }

        public async Task<decimal> Total()
        {
            return await (from i in _dbSet
                          select i.Total).SumAsync(j => j);
        }

        public async Task<HighestAverageSalesValue?> HighestAverageSalesValue()
        {
            return (from i in _dbSet
                    from j in i.ListProductOrder
                    group j by j.OrderId into g
                    orderby g.Average(n => n.SubTotal) descending
                    select new HighestAverageSalesValue
                    (
                        g.Key,
                        g.Average(k => k.SubTotal),
                        g.Sum(l => l.SubTotal),
                        g.Sum(m => m.Quantity)
                    )).FirstOrDefault();
        }

        public async Task<OutputBrandBestSeller?> BrandBestSeller()
        {
            return (from i in _dbSet
                    from j in i.ListProductOrder
                    group j by j.Product.Id into g
                    select new OutputBrandBestSeller(
                        g.Key,
                        g.Sum(i => i.Quantity),
                        g.Sum(i => i.SubTotal)
                        )).ToList().OrderByDescending(i => i.TotalSell).FirstOrDefault();
        }

        public async Task<OutputCustomerOrder?> BiggestBuyer()
        {
            return (from i in _dbSet
                    from j in i.ListProductOrder
                    group j by i.CustomerId into g
                    orderby g.Sum(i => i.Quantity) descending
                    select new OutputCustomerOrder
                    (
                        g.Key,
                        g.Sum(i => i.OrderId),
                        g.Sum(p => p.Quantity),
                        g.Sum(p => p.SubTotal)
                    )).FirstOrDefault();
        }
    }
}