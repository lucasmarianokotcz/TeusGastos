using Microsoft.EntityFrameworkCore;
using TeusGastos.Shared.Contexto;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;

namespace TeusGastos.Shared.Servicos.MercadoServico;

public class MercadoServicoRepositorio(AppDbContext context) : IMercadoServico
{
    public async Task<IEnumerable<Mercado>> ObterTodos(CancellationToken cancellationToken)
    {
        return await context.Mercados.ToListAsync(cancellationToken);
    }
    
    public async Task<ItensPaginados<Mercado>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        var query = context.Mercados.AsQueryable();

        int? idBuscado = null;
        if (!string.IsNullOrWhiteSpace(busca) && int.TryParse(busca, out int id))
            idBuscado = id;

        // Aplicação de filtros
        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(element =>
                (idBuscado.HasValue && idBuscado.Value == element.Id) ||
                element.Nome.Contains(busca) ||
                (!string.IsNullOrEmpty(element.Endereco) && element.Endereco.Contains(busca)));
        }

        // Verifica se foi passado um nome de campo para ordenação
        if (!string.IsNullOrEmpty(ordenarPor))
        {
            query = ordenarPor switch
            {
                "Id" => ordemCrescente ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id),
                "Nome" => ordemCrescente ? query.OrderBy(x => x.Nome) : query.OrderByDescending(x => x.Nome),
                "Endereco" => ordemCrescente ? query.OrderBy(x => x.Endereco) : query.OrderByDescending(x => x.Endereco),
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
        return new ItensPaginados<Mercado>()
        {
            Itens = itens,
            Total = totalCount
        };
    }

    public async Task<Mercado?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        return await context.Mercados.FindAsync(id, cancellationToken);
    }

    public async Task InserirOuAlterar(Mercado mercado, CancellationToken cancellationToken)
    {
        if (mercado.Id == 0)
            await context.Mercados.AddAsync(mercado, cancellationToken);
        else
            context.Mercados.Update(mercado);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Excluir(int id, CancellationToken cancellationToken)
    {
        var mercado = await ObterPorId(id, cancellationToken);
        if (mercado is not null)
        {
            context.Mercados.Remove(mercado);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}