using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Mapeamentos;

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
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UnidadeMedidaMapeamento());
        modelBuilder.ApplyConfiguration(new MercadoMapeamento());
        modelBuilder.ApplyConfiguration(new ProdutoMapeamento());
        modelBuilder.ApplyConfiguration(new NotaCompraMapeamento());
        modelBuilder.ApplyConfiguration(new ItemNotaCompraMapeamento());
    }
}