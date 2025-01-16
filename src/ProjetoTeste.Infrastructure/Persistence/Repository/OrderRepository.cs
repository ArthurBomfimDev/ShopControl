using Microsoft.EntityFrameworkCore;
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
            return (from i in _dbSet
                    select i.Total).Count();
        }
        //public void teste2()
        //{
        //    var totalSeller = (from i in _dbSet
        //                       from j in i.ListProductOrder
        //                       group j by j.Product.BrandId into g
        //                       let totalSaleQuantity = g.Sum(i => i.Quantity)
        //                       let totalSaleValue = g.Sum(i => i.SubTotal)
        //                       select new OutputMaxSaleValueProduct(g.Key.Id, g.Key.Name, g.Key.Code, g.Key.Description, totalSaleValue, g.Key.BrandId, totalSaleQuantity)).OrderByDescending(i => i.TotalValue).ToList();

        //var totalSeller2 = (from i in _dbSet
        //                    from j in i.ListProductOrder
        //                    join product in _context.Product on j.ProductId equals product.Id
        //                    join brand in _context.Brand on product.BrandId equals brand.Id
        //                    group j by brand into g
        //                    let totalSaleQuantity = g.Sum(i => i.Quantity)
        //                    let totalSaleValue = g.Sum(i => i.SubTotal)
        //                    select new
        //                    {
        //                        BrandCode = g.Key.Code,
        //                        BrandDescription = g.Key.Description,
        //                        TotalValue = totalSaleValue
        //                    }).OrderByDescending(i => i.TotalValue).FirstOrDefault();
        //}


        //public async Task<List<Order>> GetProductOrdersLINQ()
        //{
        //    var get = await _context.Set<Order>()
        //        .Include(o => o.Customer)
        //        .Include(o => o.ListProductOrder)
        //        .ThenInclude(po => po.Product).ToListAsync();
        //    return get;
        //}
    }
}