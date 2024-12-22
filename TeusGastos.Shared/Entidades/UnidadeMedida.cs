namespace TeusGastos.Shared.Entidades;

public class UnidadeMedida
{
    public const int NomeMaxLength = 100;
    public const int SiglaMaxLength = 20;
    
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;

    // Relacionamento com Produtos
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
