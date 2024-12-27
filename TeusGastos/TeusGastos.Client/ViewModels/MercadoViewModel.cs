using System.ComponentModel.DataAnnotations;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Client.ViewModels;

public class MercadoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(Mercado.NomeMaxLength, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres.")]
    public string Nome { get; set; } = string.Empty;
    
    [StringLength(Mercado.EnderecoMaxLength, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres.")]
    public string? Endereco { get; set; }
}