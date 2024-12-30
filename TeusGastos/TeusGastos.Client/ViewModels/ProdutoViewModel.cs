using System.ComponentModel.DataAnnotations;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Client.ViewModels;

public class ProdutoViewModel
{
    public int Id { get; set; }

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(Produto.DescricaoMaxLength, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres.")]
    public string Descricao { get; set; } = string.Empty;
    
    [Display(Name = "Código EAN")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(Produto.CodigoEANLength,
        MinimumLength = Produto.CodigoEANLength,
        ErrorMessage = "O campo {0} precisa ter exatamente {1} caracteres.")]
    public string CodigoEAN { get; set; } = string.Empty;

    [Display(Name = "Unidade de Medida")]
    [Required(ErrorMessage = "Selecione uma {0}.")]
    public string? UnidadeMedida { get; set; }
}