using Microsoft.EntityFrameworkCore;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Contexto;

public class AppDbContext : DbContext
{
    public DbSet<UnidadeMedida> UnidadesMedida { get; set; } = null!;
    public DbSet<Mercado> Mercados { get; set; } = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;
    public DbSet<NotaCompra> NotasCompra { get; set; } = null!;
    public DbSet<ItemNotaCompra> ItensNotaCompra { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=LUCASAMANDA\\SQLEXPRESS;Database=TeusGastos;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UnidadeMedida>()
            .HasMany(u => u.Produtos)
            .WithOne(p => p.Unidade)
            .HasForeignKey(p => p.UnidadeId);

        modelBuilder.Entity<Mercado>()
            .HasMany(m => m.NotasCompra)
            .WithOne(n => n.Mercado)
            .HasForeignKey(n => n.MercadoId);

        modelBuilder.Entity<Produto>()
            .HasMany(p => p.ItensNotaCompra)
            .WithOne(i => i.Produto)
            .HasForeignKey(i => i.ProdutoId);

        modelBuilder.Entity<NotaCompra>()
            .HasMany(n => n.ItensNotaCompra)
            .WithOne(i => i.NotaCompra)
            .HasForeignKey(i => i.NotaId);

        modelBuilder.Entity<ItemNotaCompra>()
            .Property(i => i.ValorTotal)
            .HasComputedColumnSql("[Quantidade] * ([ValorUnitario] - [Desconto])");
    }
}
