using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pedidos.Application.Dto.Pedido;

public sealed class CriarPedidoDto
{
    [Required(ErrorMessage ="A data do pedido deve ser informada")]
    public DateTime DataPedido { get; set; }
    [Required(ErrorMessage = "Id do cliente nao informado")]
    public Guid IdCliente { get; set; }
    public CriarEnderecoDto? EnderecoEntrega { get; set; }
    public int StatusEnum { get; set; }
    public List<CriarItemPedidoDto>? Itens { get; set; }
    public decimal VlTotal { get; set; }
}
