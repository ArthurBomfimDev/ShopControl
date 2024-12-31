using ProjetoTeste.Infrastructure.Persistence.Repository;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Order : BaseEntity
{
    protected readonly ProductRepository _repository;

    public Order(ProductRepository repository)
    {
        _repository = repository;
    }
    public long ClientId { get; set; }
    public Client Client { get; set; }
    public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    public DateOnly OrderDate { get; set; }
    //public decimal Total
    //{
    //    get
    //    {
    //        return Total;
    //    }
    //    set
    //    {
    //        var products = _repository.GetAll();
    //        var ids = ProductOrders.Select(p => p.ProductId).ToList();
    //        var quantitys = ProductOrders.Select(p => p.Quantity).ToList();
    //        Total = products
    //            .Where(product => ids.Contains(product.Id)) // Filtra os produtos com IDs correspondentes
    //            .Select(product =>
    //                product.Price * ProductOrders.First(order => order.ProductId == product.Id).Quantity
    //            )
    //            .Sum();
    //    }
    //}
    public decimal Total { get; set; }
    public Order()
    { }

    public Order(long clientId, DateOnly orderDate)
    {
        ClientId = clientId;
        OrderDate = orderDate;
    }
}
