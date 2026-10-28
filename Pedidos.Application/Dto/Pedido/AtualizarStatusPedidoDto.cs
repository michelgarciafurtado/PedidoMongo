using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Application.Dto.Pedido;

public record AtualizarStatusPedidoDto
{
    Guid Id,
    StatusPedidoEnum Status
}
