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

        builder.Property(c => c.Name).HasColumnName("nome");
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(64);

        builder.Property(c => c.Email).HasColumnName("email");
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(64);

        builder.Property(c => c.CPF).HasColumnName("cpf");
        builder.Property(c => c.CPF).IsRequired();
        builder.Property(c => c.CPF).HasMaxLength(15);

        builder.Property(c => c.Phone).HasColumnName("telefone");
        builder.Property(c => c.Phone).IsRequired();
        builder.Property(c => c.Phone).HasMaxLength(15);
    }
}