using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;

namespace TeusGastos.Shared.Servicos.ProdutoServico;

public interface IProdutoServico
{
    Task<IEnumerable<Produto>> ObterTodos(CancellationToken cancellationToken);
    Task<ItensPaginados<Produto>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken);
    Task<Produto?> ObterPorId(int id, CancellationToken cancellationToken);
    Task InserirOuAlterar(Produto produto, CancellationToken cancellationToken);
    Task Excluir(int id, CancellationToken cancellationToken);
}