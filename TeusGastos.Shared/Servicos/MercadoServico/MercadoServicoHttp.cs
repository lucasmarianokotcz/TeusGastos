using System.Net.Http.Json;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;

namespace TeusGastos.Shared.Servicos.MercadoServico;

public class MercadoServicoHttp(HttpClient httpClient) : IMercadoServico
{
    private const string RelativeUri = "/api/mercados";
    
    public async Task<IEnumerable<Mercado>> ObterTodos(CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<Mercado>>(RelativeUri, cancellationToken) ?? [];
    }

    public async Task<ItensPaginados<Mercado>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<ItensPaginados<Mercado>>(
                   $"{RelativeUri}/com-paginacao?busca={busca}&pagina={pagina}&tamanhoPagina={tamanhoPagina}&ordenarPor={ordenarPor}&ordemCrescente={ordemCrescente}&cancellationToken={cancellationToken}",
                   cancellationToken) ??
               new ItensPaginados<Mercado>();
    }

    public async Task<Mercado?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<Mercado?>($"{RelativeUri}/{id}?cancellationToken={cancellationToken}",
            cancellationToken);
    }

    public async Task InserirOuAlterar(Mercado mercado, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(RelativeUri, mercado, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task Excluir(int id, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync($"{RelativeUri}/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}