using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Persistence.Entity;

public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.OrderId).HasColumnName("pedido_id")
            .IsRequired();

        builder.Property(p => p.ProductId).HasColumnName("produto_id")
            .IsRequired();

        builder.Property(p => p.Quantity).HasColumnName("quantidade")
            .IsRequired();

        builder.Property(p => p.UnitPrice).HasColumnName("preco_unitario")
            .IsRequired();

        builder.Property(p => p.SubTotal).HasColumnName("subtotal")
            .HasComputedColumnSql("quantidade * preco_unitario"); 
    }
}
