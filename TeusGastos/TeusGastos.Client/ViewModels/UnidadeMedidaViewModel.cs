using System.ComponentModel.DataAnnotations;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Client.ViewModels;

public class UnidadeMedidaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(UnidadeMedida.NomeMaxLength, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres.")]
    public string Nome { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(UnidadeMedida.SiglaMaxLength, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres.")]
    public string Sigla { get; set; } = string.Empty;
}