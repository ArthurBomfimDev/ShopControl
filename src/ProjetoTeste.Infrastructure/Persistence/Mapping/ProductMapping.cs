using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.Brand).WithMany(b => b.ListProduct).HasForeignKey(p => p.BrandId).HasConstraintName("fkey_id_marca");

        builder.ToTable("produto");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasColumnName("nome");
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(40);

        builder.Property(p => p.Code).HasColumnName("codigo");
        builder.Property(p => p.Code).IsRequired();
        builder.Property(p => p.Code).HasMaxLength(6);

        builder.Property(p => p.Description).HasColumnName("descricao");
        builder.Property(p => p.Description).HasMaxLength(100);

        builder.Property(p => p.BrandId).HasColumnName("id_marca");
        builder.Property(p => p.BrandId).IsRequired();
    }
}