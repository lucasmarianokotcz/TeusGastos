using Microsoft.AspNetCore.Mvc;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;
using TeusGastos.Shared.Servicos.UnidadeMedidaServico;

namespace TeusGastos.Controllers;

[ApiController]
[Route("api/unidades-de-medida")]
public class UnidadesMedidaController(IUnidadeMedidaServico servico) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnidadeMedida>>> ObterTodos(CancellationToken cancellationToken)
    {
        return Ok(await servico.ObterTodos(cancellationToken));
    }

    [Route("com-paginacao")]
    [HttpGet]
    public async Task<ActionResult<ItensPaginados<UnidadeMedida>>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        return Ok(await servico.ObterComPaginacao(busca, pagina, tamanhoPagina, ordenarPor, ordemCrescente, cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UnidadeMedida>> ObterPorId(int id, CancellationToken cancellationToken)
    {
        var unidade = await servico.ObterPorId(id, cancellationToken);
        if (unidade == null) return NotFound();
        return Ok(unidade);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarOuAlterar(UnidadeMedida unidade, CancellationToken cancellationToken)
    {
        await servico.InserirOuAlterar(unidade, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id, CancellationToken cancellationToken)
    {
        await servico.Excluir(id, cancellationToken);
        return NoContent();
    }
}