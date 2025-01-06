using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entity;


namespace ProjetoTeste.Infrastructure.Persistence.Mapping;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("cliente");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(c => c.Email).HasColumnName("email")
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(c => c.CPF).HasColumnName("cpf")
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength();

        builder.Property(c => c.Phone).HasColumnName("telefone")
            .IsRequired()
            .HasMaxLength(15)
            .IsFixedLength();
    }
}