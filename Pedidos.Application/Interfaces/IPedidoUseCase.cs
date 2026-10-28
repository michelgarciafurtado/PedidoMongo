using Pedidos.Application.Dto.Pedido;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Application.Interfaces;

public interface IPedidoUseCase
{
    Task ICriarPedido(CriarPedidoDto request);
}
