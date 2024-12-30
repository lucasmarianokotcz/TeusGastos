namespace TeusGastos.Shared.Entidades;

public class Produto
{
    public const int CodigoEANLength = 13;
    public const int DescricaoMaxLength = 255;
    
    public int Id { get; set; }
    public string CodigoEAN { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int UnidadeId { get; set; }

    // Relacionamento com UnidadeMedida
    public UnidadeMedida? Unidade { get; set; }

    // Relacionamento com ItensNotaCompra
    public ICollection<ItemNotaCompra> ItensNotaCompra { get; set; } = new List<ItemNotaCompra>();
}
