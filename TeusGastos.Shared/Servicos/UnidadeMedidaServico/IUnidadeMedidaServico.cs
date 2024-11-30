using TeusGastos.Shared.Entidades;

namespace TeusGastos.Shared.Servicos.UnidadeMedidaServico;

public interface IUnidadeMedidaServico
{
    Task<IEnumerable<UnidadeMedida>> ObterTodos();
    Task<UnidadeMedida?> ObterPorId(int id);
    Task InserirOuAlterar(UnidadeMedida unidade);
    Task Excluir(int id);
}