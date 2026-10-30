using PedidosMongo.Application.Dto.Pedido;
using PedidosMongo.Application.Interfaces;
using PedidosMongo.Domain.Interfaces;
using PedidosMongo.Domain.Agregados;
using PedidosMongo.Domain.Entidades;
using PedidosMongo.Domain.Enuns;
using PedidosMongo.Domain.ObjetosValor;

namespace PedidosMongo.Application.UseCase.Pedidos;

public sealed class PedidosUseCase(IPedidoRepository pedidoRepository) : IPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
    public async Task CriarPedido(CriarPedidoDto dto)
    {
        var itensDoDominio = dto.Itens?
       .Select(itemDto => new Item(
           itemDto.ProdutoId,
           itemDto.NomeProduto,
           itemDto.Valor,
           itemDto.Qtd
       )).ToList() ?? new List<Item>();


        PedidosMongo.Domain.Agregados.Pedidos novoPedido = new
             PedidosMongo.Domain.Agregados.Pedidos(dto.DataPedido,
             dto.IdCliente,
             new Cliente(dto.IdCliente, dto.NomeCliente),
             new Endereco(dto.EnderecoEntrega.Rua,
                                dto.EnderecoEntrega.n,dto.EnderecoEntrega.Bairro,
                                dto.EnderecoEntrega.Cidade, dto.EnderecoEntrega.Cep,
                                dto.EnderecoEntrega.compl),
             (StatusPedidoEnum)dto.StatusEnum,
             itensDoDominio
             );

        _pedidoRepository.CriarPedidoAsync(novoPedido);
    }
}
