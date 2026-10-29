using PedidosMongo.Domain.Entidades;

namespace PedidosMongo.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task CriarAsync(Categoria categoria);
    Task<List<Categoria>> ObterTodasAsync();
}
