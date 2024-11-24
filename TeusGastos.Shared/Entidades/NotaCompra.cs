namespace TeusGastos.Shared.Entidades;

public class NotaCompra
{
    public int Id { get; set; }
    public int MercadoId { get; set; }
    public DateTime DataCompra { get; set; }
    public decimal Total { get; set; }

    // Relacionamento com Mercado
    public Mercado Mercado { get; set; } = null!;

    // Relacionamento com ItensNotaCompra
    public ICollection<ItemNotaCompra> ItensNotaCompra { get; set; } = new List<ItemNotaCompra>();
}
