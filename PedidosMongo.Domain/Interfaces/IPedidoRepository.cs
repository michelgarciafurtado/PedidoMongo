using PedidosMongo.Domain.Agregados;

namespace PedidosMongo.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task ICriarPedido(Pedidos pedido);
    }
}
