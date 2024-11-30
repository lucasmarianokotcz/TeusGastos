using System.Net.Http.Json;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Servicos.UnidadeMedidaServico;

public class UnidadeMedidaServicoHttp(HttpClient httpClient) : IUnidadeMedidaServico
{
    private const string RelativeUri = "/api/unidades-de-medida";
    
    public async Task<IEnumerable<UnidadeMedida>> ObterTodos()
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<UnidadeMedida>>(RelativeUri) ?? [];
    }

    public async Task<UnidadeMedida?> ObterPorId(int id)
    {
        return await httpClient.GetFromJsonAsync<UnidadeMedida?>($"{RelativeUri}/{id}");
    }

    public async Task InserirOuAlterar(UnidadeMedida unidade)
    {
        var response = await httpClient.PostAsJsonAsync(RelativeUri, unidade);
        response.EnsureSuccessStatusCode();
    }

    public async Task Excluir(int id)
    {
        var response = await httpClient.DeleteAsync($"{RelativeUri}/{id}");
        response.EnsureSuccessStatusCode();
    }
}