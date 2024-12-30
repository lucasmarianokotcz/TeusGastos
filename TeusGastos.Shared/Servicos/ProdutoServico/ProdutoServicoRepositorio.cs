using Microsoft.EntityFrameworkCore;
using TeusGastos.Shared.Contexto;
using TeusGastos.Shared.Entidades;
using TeusGastos.Shared.Entidades.Common;

namespace TeusGastos.Shared.Servicos.ProdutoServico;

public class ProdutoServicoRepositorio(AppDbContext context) : IProdutoServico
{
    public async Task<IEnumerable<Produto>> ObterTodos(CancellationToken cancellationToken)
    {
        return await context.Produtos.ToListAsync(cancellationToken);
    }
    
    public async Task<ItensPaginados<Produto>> ObterComPaginacao(
        string? busca, int pagina, int tamanhoPagina, string? ordenarPor, bool ordemCrescente, CancellationToken cancellationToken)
    {
        var query = context.Produtos.AsQueryable();

        int? idBuscado = null;
        if (!string.IsNullOrWhiteSpace(busca) && int.TryParse(busca, out int id))
            idBuscado = id;

        // Aplicação de filtros
        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(element =>
                (idBuscado.HasValue && idBuscado.Value == element.Id) ||
                element.Descricao.Contains(busca) ||
                element.CodigoEAN.Contains(busca));
        }

        // Verifica se foi passado um nome de campo para ordenação
        if (!string.IsNullOrEmpty(ordenarPor))
        {
            query = ordenarPor switch
            {
                "Id" => ordemCrescente ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id),
                "Descricao" => ordemCrescente ? query.OrderBy(x => x.Descricao) : query.OrderByDescending(x => x.Descricao),
                "CodigoEAN" => ordemCrescente ? query.OrderBy(x => x.CodigoEAN) : query.OrderByDescending(x => x.CodigoEAN),
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
        return new ItensPaginados<Produto>()
        {
            Itens = itens,
            Total = totalCount
        };
    }

    public async Task<Produto?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        return await context.Produtos.FindAsync(id, cancellationToken);
    }

    public async Task InserirOuAlterar(Produto produto, CancellationToken cancellationToken)
    {
        if (produto.Id == 0)
            await context.Produtos.AddAsync(produto, cancellationToken);
        else
            context.Produtos.Update(produto);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Excluir(int id, CancellationToken cancellationToken)
    {
        var Produto = await ObterPorId(id, cancellationToken);
        if (Produto is not null)
        {
            context.Produtos.Remove(Produto);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}