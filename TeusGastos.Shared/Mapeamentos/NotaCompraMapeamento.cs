using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Mapeamentos;

public class NotaCompraMapeamento : IEntityTypeConfiguration<NotaCompra>
{
    public void Configure(EntityTypeBuilder<NotaCompra> builder)
    {
        builder.ToTable("NotaCompra");
        
        builder.HasKey(n => n.Id);
        
        builder.Property(n => n.Total)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();
        
        builder.Property(n => n.DataCompra)
            .HasColumnType("date")
            .IsRequired();
        
        builder.HasMany(n => n.ItensNotaCompra)
            .WithOne(i => i.NotaCompra)
            .HasForeignKey(i => i.NotaId);
    }
}