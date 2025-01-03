using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Mapeamentos;

public class MercadoMapeamento : IEntityTypeConfiguration<Mercado>
{
    public void Configure(EntityTypeBuilder<Mercado> builder)
    {
        builder.ToTable("Mercado");
        
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Nome)
            .HasColumnType($"varchar({Mercado.NomeMaxLength})")
            .IsRequired();
        
        builder.Property(m => m.Endereco)
            .HasColumnType($"varchar({Mercado.EnderecoMaxLength})")
            .IsRequired(false);
        
        builder.HasMany(m => m.NotasCompra)
            .WithOne(n => n.Mercado)
            .HasForeignKey(n => n.MercadoId);
    }
}