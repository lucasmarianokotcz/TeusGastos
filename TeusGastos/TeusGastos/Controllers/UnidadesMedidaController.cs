using Microsoft.AspNetCore.Mvc;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Servicos.UnidadeMedidaServico;

namespace TeusGastos.Controllers;

[ApiController]
[Route("api/unidades-de-medida")]
public class UnidadesMedidaController(IUnidadeMedidaServico servico) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UnidadeMedida>>> GetAll()
    {
        return Ok(await servico.ObterTodos());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadeMedida>> GetById(int id)
    {
        var unidade = await servico.ObterPorId(id);
        if (unidade == null) return NotFound();
        return Ok(unidade);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdate(UnidadeMedida unidade)
    {
        await servico.InserirOuAlterar(unidade);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await servico.Excluir(id);
        return NoContent();
    }
}