using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Application.Dto.Pedido;

public record CriarItemPedidoDto(
 Guid ProdutoId,
 int Qtd
);
