using ProjetoEstagioAPI.Infrastructure.Products;

namespace ProjetoEstagioAPI.Models;

public class Order : BaseEntity
{
    protected readonly ProductRepository _repository;

    public Order(ProductRepository repository)
    {
        _repository = repository;
    }
    public long ClientId { get; set; }
    public Client Client { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total
    {
        get
        {
            return Total;
        }
        set
        {
            var products = _repository.GetAll();
            var ids = ProductOrders.Select(p => p.ProductId).ToList();
            var quantitys = ProductOrders.Select(p => p.Quantity).ToList();
            Total = products
                .Where(product => ids.Contains(product.Id)) // Filtra os produtos com IDs correspondentes
                .Select(product =>
                    product.Price * ProductOrders.First(order => order.ProductId == product.Id).Quantity
                )
                .Sum();
        }
    }
    public List<ProductOrder> ProductOrders { get; set; }
    public Order()
    { }

    public Order(long clientId, DateTime orderDate, List<ProductOrder> productOrders)
    {
        ClientId = clientId;
        OrderDate = orderDate;
        ProductOrders = productOrders;
    }
}
