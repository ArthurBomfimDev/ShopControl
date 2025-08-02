using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopControl.Infrastructure.Persistence.Entity;

public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        builder.HasOne(p => p.Order).WithMany(o => o.ListProductOrder).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(p => p.Product).WithMany(o => o.ListProductOrder).HasForeignKey(o => o.ProductId);

        builder.ToTable("pedido_de_produto");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.OrderId).HasColumnName("pedido_id");
        builder.Property(p => p.OrderId).IsRequired();

        builder.Property(p => p.ProductId).HasColumnName("produto_id");
        builder.Property(p => p.ProductId).IsRequired();

        builder.Property(p => p.Quantity).HasColumnName("quantidade");
        builder.Property(p => p.Quantity).IsRequired();

        builder.Property(p => p.UnitPrice).HasColumnName("preco_unitario");
        builder.Property(p => p.UnitPrice).IsRequired();

        builder.Property(p => p.SubTotal).HasColumnName("subtotal");
        builder.Property(p => p.SubTotal).IsRequired();
    }
}