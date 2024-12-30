using System.Net.Http.Json;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;

namespace TeusGastos.Shared.Servicos.ProdutoServico;

public class ProdutoServicoHttp(HttpClient httpClient) : IProdutoServico
{
    private const string RelativeUri = "/api/produtos";
    
    public async Task<IEnumerable<Produto>> ObterTodos(CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<Produto>>(RelativeUri, cancellationToken) ?? [];
    }

    public async Task<ItensPaginados<Produto>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<ItensPaginados<Produto>>(
                   $"{RelativeUri}/com-paginacao?busca={busca}&pagina={pagina}&tamanhoPagina={tamanhoPagina}&ordenarPor={ordenarPor}&ordemCrescente={ordemCrescente}&cancellationToken={cancellationToken}",
                   cancellationToken) ??
               new ItensPaginados<Produto>();
    }

    public async Task<Produto?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<Produto?>($"{RelativeUri}/{id}?cancellationToken={cancellationToken}",
            cancellationToken);
    }

    public async Task InserirOuAlterar(Produto produto, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(RelativeUri, produto, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task Excluir(int id, CancellationToken cancellationToken)
    {
        var response = await httpClient.DeleteAsync($"{RelativeUri}/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}