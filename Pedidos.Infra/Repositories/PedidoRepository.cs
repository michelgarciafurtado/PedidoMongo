using MongoDB.Driver;
using PedidosMongo.Domain.Agregados;
using PedidosMongo.Domain.Interfaces;

namespace PedidosMongo.Infra.Repositories;

public sealed class PedidoRepository : IPedidoRepository
{
    private readonly IMongoCollection<Pedidos> _pedidosCollection;

    public PedidoRepository(IMongoDatabase database)
    {
        _pedidosCollection = database.GetCollection<Pedidos>("Pedidos");
    }

    public async Task CriarPedidoAsync(Pedidos pedido)
    {
        await _pedidosCollection.InsertOneAsync(pedido);
    }

    public async Task<List<Pedidos>> ObterTodosPedidosAsync()
    {
        return await _pedidosCollection.Find(_ => true)
                    .ToListAsync();
    }
}
