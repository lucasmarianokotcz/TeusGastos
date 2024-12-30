using Microsoft.AspNetCore.Mvc;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;
using TeusGastos.Shared.Servicos.ProdutoServico;

namespace TeusGastos.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController(IProdutoServico servico) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> ObterTodos(CancellationToken cancellationToken)
    {
        return Ok(await servico.ObterTodos(cancellationToken));
    }

    [Route("com-paginacao")]
    [HttpGet]
    public async Task<ActionResult<ItensPaginados<Produto>>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        return Ok(await servico.ObterComPaginacao(busca, pagina, tamanhoPagina, ordenarPor, ordemCrescente, cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> ObterPorId(int id, CancellationToken cancellationToken)
    {
        var unidade = await servico.ObterPorId(id, cancellationToken);
        if (unidade == null) return NotFound();
        return Ok(unidade);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarOuAlterar(Produto produto, CancellationToken cancellationToken)
    {
        await servico.InserirOuAlterar(produto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id, CancellationToken cancellationToken)
    {
        await servico.Excluir(id, cancellationToken);
        return NoContent();
    }
}