using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.Customer).WithMany(p => p.ListOrder).HasForeignKey(o => o.CustomerId);

        builder.ToTable("pedido");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CustomerId).HasColumnName("cliente_id");
        builder.Property(o => o.CustomerId).IsRequired();


        builder.Property(o => o.OrderDate).HasColumnName("data_do_pedido");
        builder.Property(o => o.OrderDate).IsRequired();

        builder.Property(o => o.Total).HasColumnName("total");
        builder.Property(o => o.Total).IsRequired();
    }
}