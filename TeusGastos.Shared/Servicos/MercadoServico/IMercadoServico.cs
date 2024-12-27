using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;

namespace TeusGastos.Shared.Servicos.MercadoServico;

public interface IMercadoServico
{
    Task<IEnumerable<Mercado>> ObterTodos(CancellationToken cancellationToken);
    Task<ItensPaginados<Mercado>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken);
    Task<Mercado?> ObterPorId(int id, CancellationToken cancellationToken);
    Task InserirOuAlterar(Mercado mercado, CancellationToken cancellationToken);
    Task Excluir(int id, CancellationToken cancellationToken);
}