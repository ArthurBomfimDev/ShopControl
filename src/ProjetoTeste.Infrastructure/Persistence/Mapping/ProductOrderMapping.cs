using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping;

public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.OrderId).HasColumnName("pedido_id")
            .IsRequired();
    }
}
