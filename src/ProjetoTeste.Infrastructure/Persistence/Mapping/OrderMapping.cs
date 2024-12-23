using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("pedidos");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.ClientId).HasColumnName("cliente_id")
            .IsRequired();

        builder.HasOne(o => o.Client)
            .WithMany(p => p.Order)
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.OrderDate).HasColumnName("data_do_pedido")
            .IsRequired();

        builder.Property(o => o.Total).HasColumnName("total")
            .IsRequired();

    }
}