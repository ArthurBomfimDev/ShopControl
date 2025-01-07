using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("pedidos");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CustomerId).HasColumnName("cliente_id")
            .IsRequired();

        builder.HasOne(o => o.Customer)
            .WithMany(p => p.ListOrder)
            .HasForeignKey(o => o.CustomerId);

        builder.Property(o => o.OrderDate).HasColumnName("data_do_pedido")
            .IsRequired();

        builder.Property(o => o.Total).HasColumnName("total")
            .IsRequired();
    }
}