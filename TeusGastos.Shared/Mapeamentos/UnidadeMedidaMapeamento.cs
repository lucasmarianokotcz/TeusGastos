using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Mapeamentos;

public class UnidadeMedidaMapeamento : IEntityTypeConfiguration<UnidadeMedida>
{
    public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
    {
        builder.ToTable("UnidadeMedida");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Nome)
            .HasColumnType($"varchar({UnidadeMedida.NomeMaxLength})")
            .IsRequired();
        
        builder.Property(u => u.Sigla)
            .HasColumnType($"varchar({UnidadeMedida.SiglaMaxLength})")
            .IsRequired();
        
        builder.HasMany(u => u.Produtos)
            .WithOne(p => p.Unidade)
            .HasForeignKey(p => p.UnidadeId);
    }
}