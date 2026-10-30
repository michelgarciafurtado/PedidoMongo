using PedidosMongo.Domain.Agregados;

namespace PedidosMongo.Domain.Interfaces;

public interface IPedidoRepository
{
    Task CriarPedidoAsync(Pedidos pedido);
    Task<List<Pedidos>> ObterTodosPedidosAsync();
}
