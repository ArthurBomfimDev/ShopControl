using Microsoft.EntityFrameworkCore;
using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Order;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository;
using ShopControl.Infrastructure.Persistence.Context;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Infrastructure.Persistence.Repository
{
    public class OrderRepository : BaseRepository<Order, OrderDTO>, IOrderRepository
    {
        private readonly IProductOrderRepository _productOrderRepository;

        public OrderRepository(AppDbContext context, IProductOrderRepository productOrderRepository) : base(context)
        {
            _productOrderRepository = productOrderRepository;
        }

        public async Task<List<OrderDTO>> GetAllWithProductOrders()
        {
            var listOrder = await _dbSet.Select(x => new Order
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                ListProductOrder = x.ListProductOrder,
                OrderDate = x.OrderDate,
                Total = x.Total,
                Customer = x.Customer
            }).ToListAsync();

            return listOrder.Select(i => (OrderDTO)i).ToList();
        }
        public async Task<List<OrderDTO>> GetByIdWithProductOrders(long id)
        {
            var getByIdWithProductOrders = await _dbSet.AsNoTracking().Include(o => o.ListProductOrder)
                .Where(o => o.Id == id).ToListAsync();
            return getByIdWithProductOrders.Select(i => (OrderDTO)i).ToList();
        }
        public async Task<List<OrderDTO>> GetListByListIdWhithProductOrders(List<long> listId)
        {
            var getListByListIdWhithProductOrders = await _dbSet.Include(i => i.ListProductOrder).Where(j => listId.Contains(j.Id)).ToListAsync();
            return getListByListIdWhithProductOrders.Select(i => (OrderDTO)i).ToList();
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
                    group j by j.Product.BrandId into g
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