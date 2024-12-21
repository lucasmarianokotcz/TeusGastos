namespace TeusGastos.Shared.Entidades;

public class Produto
{
    public int Id { get; set; }
    public string CodigoEAN { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int UnidadeId { get; set; }

    // Relacionamento com UnidadeMedida
    public UnidadeMedida Unidade { get; set; } = null!;

    // Relacionamento com ItensNotaCompra
    public ICollection<ItemNotaCompra> ItensNotaCompra { get; set; } = new List<ItemNotaCompra>();
}
