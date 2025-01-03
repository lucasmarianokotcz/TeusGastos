using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Mapeamentos;

public class ProdutoMapeamento : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.CodigoEAN)
            .HasColumnType($"char({Produto.CodigoEANLength})")
            .IsRequired();
        
        builder.Property(p => p.Descricao)
            .HasColumnType($"varchar({Produto.DescricaoMaxLength})")
            .IsRequired();
        
        builder.HasMany(p => p.ItensNotaCompra)
            .WithOne(i => i.Produto)
            .HasForeignKey(i => i.ProdutoId);
    }
}