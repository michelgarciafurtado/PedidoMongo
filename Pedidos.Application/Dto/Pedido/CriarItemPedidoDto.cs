using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Dto.Pedido;

public record CriarItemPedidoDto(
 Guid ProdutoId,
 string NomeProduto,
 decimal Valor,
 int Qtd
);
