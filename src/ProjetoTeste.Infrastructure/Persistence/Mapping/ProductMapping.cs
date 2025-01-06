using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(24);

        builder.Property(p => p.Code).HasColumnName("codigo")
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(p => p.Description).HasColumnName("descricao")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.BrandId).HasColumnName("marca_id")
            .IsRequired();

        builder.HasOne(p => p.Brand)
            .WithMany(b => b.ListProduct)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}