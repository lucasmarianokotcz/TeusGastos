namespace TeusGastos.Shared.Entidades.Common;

public class ItensPaginados<T> where T : class
{
    public IEnumerable<T> Itens { get; set; } = new List<T>();
    public int Total { get; set; }
}