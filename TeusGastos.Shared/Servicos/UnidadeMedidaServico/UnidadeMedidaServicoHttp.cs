using System.Net.Http.Json;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Servicos.UnidadeMedidaServico;

public class UnidadeMedidaServicoHttp(HttpClient httpClient) : IUnidadeMedidaServico
{
    private const string RelativeUri = "/api/unidades-de-medida";
    
    public async Task<IEnumerable<UnidadeMedida>> ObterTodos(CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<UnidadeMedida>>(RelativeUri, cancellationToken) ?? [];
    }

    public async Task<ItensPaginados<UnidadeMedida>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<ItensPaginados<UnidadeMedida>>(
            $"{RelativeUri}/com-paginacao?busca={busca}&pagina={pagina}&tamanhoPagina={tamanhoPagina}&ordenarPor={ordenarPor}&ordemCrescente={ordemCrescente}&cancellationToken={cancellationToken}",
            cancellationToken) ??
               new ItensPaginados<UnidadeMedida>();
    }

    public async Task<UnidadeMedida?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<UnidadeMedida?>($"{RelativeUri}/{id}?cancellationToken={cancellationToken}",
            cancellationToken);
    }

    public async Task InserirOuAlterar(UnidadeMedida unidade, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(RelativeUri, unidade, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task Excluir(int id, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync($"{RelativeUri}/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}