using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Mapeamentos;

public class ItemNotaCompraMapeamento : IEntityTypeConfiguration<ItemNotaCompra>
{
    public void Configure(EntityTypeBuilder<ItemNotaCompra> builder)
    {
        builder.ToTable("ItemNotaCompra");
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Desconto)
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);
        
        builder.Property(i => i.Quantidade)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();
        
        builder.Property(i => i.ValorUnitario)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();
        
        builder.Property(i => i.ValorTotal)
            .HasComputedColumnSql("[Quantidade] * ([ValorUnitario] - [Desconto])");
    }
}