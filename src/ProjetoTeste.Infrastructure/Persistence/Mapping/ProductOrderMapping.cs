using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;

public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        builder.ToTable("pedido_de_produtos");
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
            .IsRequired();

        builder.HasOne(p => p.Order)
            .WithMany(o => o.ListProductOrder)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Product)
            .WithMany(o => o.ListProductOrder)
            .HasForeignKey(o => o.ProductId);
    }
}