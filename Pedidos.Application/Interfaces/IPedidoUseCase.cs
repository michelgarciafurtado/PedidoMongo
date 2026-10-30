using PedidosMongo.Application.Dto.Pedido;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Interfaces;

public interface IPedidoUseCase
{
    Task CriarPedido(CriarPedidoDto dto);
}
