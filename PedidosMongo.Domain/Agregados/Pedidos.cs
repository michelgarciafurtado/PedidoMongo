using PedidosMongo.Domain.Entidades;
using PedidosMongo.Domain.Enuns;
using PedidosMongo.Domain.ObjetosValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.Agregados;

public class Pedidos:EntidadeBase
{
    public Pedidos(DateTime dataPedido, Guid idCliente, Cliente? cliente, Endereco? enderecoEntrega, StatusPedidoEnum status, List<Item>? itens)
    {
        DataPedido = dataPedido;
        IdCliente = idCliente;
        Cliente = cliente;
        EnderecoEntrega = enderecoEntrega;
        Status = status;
        Itens = itens;
        VlTotal = CalculaValorTotal();
    }

    private decimal CalculaValorTotal()
    {
        if (Itens == null || !Itens.Any())
            throw new ArgumentException("Pedido sem itens");
        var resultado = Itens.Sum(item => item.ValorUn * item.Qtd);
        return resultado;
    }

    public DateTime DataPedido { get; set; }
    public Guid IdCliente { get; set; }
    public Cliente? Cliente { get; set; }
    public Endereco? EnderecoEntrega { get; set; }
    public StatusPedidoEnum Status { get; set; }
    public List<Item>? Itens { get; set; }
    public decimal VlTotal { get; set; }
}
