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
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(u => u.Sigla)
            .HasColumnType("varchar(20)")
            .IsRequired();
        
        builder.HasMany(u => u.Produtos)
            .WithOne(p => p.Unidade)
            .HasForeignKey(p => p.UnidadeId);
    }
}