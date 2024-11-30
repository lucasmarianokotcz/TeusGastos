using Microsoft.EntityFrameworkCore;
using TeusGastos.Shared.Contexto;
using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Servicos.UnidadeMedidaServico;

public class UnidadeMedidaServicoRepositorio(AppDbContext context) : IUnidadeMedidaServico
{
    public async Task<IEnumerable<UnidadeMedida>> ObterTodos()
    {
        return await context.UnidadesMedida.ToListAsync();
    }

    public async Task<UnidadeMedida?> ObterPorId(int id)
    {
        return await context.UnidadesMedida.FindAsync(id);
    }

    public async Task InserirOuAlterar(UnidadeMedida unidade)
    {
        if (unidade.Id == 0)
            await context.UnidadesMedida.AddAsync(unidade);
        else
            context.UnidadesMedida.Update(unidade);

        await context.SaveChangesAsync();
    }

    public async Task Excluir(int id)
    {
        var unidade = await ObterPorId(id);
        if (unidade is not null)
        {
            context.UnidadesMedida.Remove(unidade);
            await context.SaveChangesAsync();
        }
    }
}