using PedidosMongo.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Dto.Pedido;

public record AtualizarStatusPedidoDto
(
    Guid Id,
    StatusPedidoEnum Status
);
