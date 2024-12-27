using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeusGastos.Shared.Contexto;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;

namespace TeusGastos.Shared.Servicos.UnidadeMedidaServico;

public class UnidadeMedidaServicoRepositorio(AppDbContext context) : IUnidadeMedidaServico
{
    public async Task<IEnumerable<UnidadeMedida>> ObterTodos(CancellationToken cancellationToken)
    {
        return await context.UnidadesMedida.ToListAsync(cancellationToken);
    }
    
    public async Task<ItensPaginados<UnidadeMedida>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        var query = context.UnidadesMedida.AsQueryable();

        int? idBuscado = null;
        if (!string.IsNullOrWhiteSpace(busca) && int.TryParse(busca, out int id))
            idBuscado = id;

        // Aplicação de filtros
        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(element =>
                (idBuscado.HasValue && idBuscado.Value == element.Id) ||
                element.Nome.Contains(busca) ||
                element.Sigla.Contains(busca));
        }

        // Verifica se foi passado um nome de campo para ordenação
        if (!string.IsNullOrEmpty(ordenarPor))
        {
            query = ordenarPor switch
            {
                "Id" => ordemCrescente ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id),
                "Nome" => ordemCrescente ? query.OrderBy(x => x.Nome) : query.OrderByDescending(x => x.Nome),
                "Sigla" => ordemCrescente ? query.OrderBy(x => x.Sigla) : query.OrderByDescending(x => x.Sigla),
                _ => query
            };
        }

        // Obtém o total de registros
        var totalCount = await query.CountAsync(cancellationToken);

        // Aplica a paginação
        var itens = await query
            .Skip(pagina * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync(cancellationToken);

        // Retorna o resultado paginado
        return new ItensPaginados<UnidadeMedida>()
        {
            Itens = itens,
            Total = totalCount
        };
    }

    public async Task<UnidadeMedida?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        return await context.UnidadesMedida.FindAsync(id, cancellationToken);
    }

    public async Task InserirOuAlterar(UnidadeMedida unidade, CancellationToken cancellationToken)
    {
        if (unidade.Id == 0)
            await context.UnidadesMedida.AddAsync(unidade, cancellationToken);
        else
            context.UnidadesMedida.Update(unidade);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Excluir(int id, CancellationToken cancellationToken)
    {
        var unidade = await ObterPorId(id, cancellationToken);
        if (unidade is not null)
        {
            context.UnidadesMedida.Remove(unidade);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}