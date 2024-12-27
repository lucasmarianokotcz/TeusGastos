namespace TeusGastos.Shared.Entidades;

public class Mercado
{
    public const int NomeMaxLength = 250;
    public const int EnderecoMaxLength = 1000;
    
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Endereco { get; set; }

    // Relacionamento com NotasCompra
    public ICollection<NotaCompra> NotasCompra { get; set; } = new List<NotaCompra>();
}
