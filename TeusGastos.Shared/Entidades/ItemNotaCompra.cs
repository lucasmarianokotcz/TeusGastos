namespace TeusGastos.Shared.Entidades;

public class ItemNotaCompra
{
    public int Id { get; set; }
    public int NotaId { get; set; }
    public int ProdutoId { get; set; }
    public decimal Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal? Desconto { get; set; }
    
    // Propriedade calculada
    public decimal ValorTotal { get; set; }

    // Relacionamentos
    public NotaCompra NotaCompra { get; set; } = null!;
    public Produto Produto { get; set; } = null!;
}
