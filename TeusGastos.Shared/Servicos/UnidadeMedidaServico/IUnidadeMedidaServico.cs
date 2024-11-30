using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Servicos.UnidadeMedidaServico;

public interface IUnidadeMedidaServico
{
    Task<IEnumerable<UnidadeMedida>> ObterTodos(CancellationToken cancellationToken);
    Task<ItensPaginados<UnidadeMedida>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken);
    Task<UnidadeMedida?> ObterPorId(int id);
    Task InserirOuAlterar(UnidadeMedida unidade, CancellationToken cancellationToken);
    Task Excluir(int id, CancellationToken cancellationToken);
}