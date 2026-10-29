using PedidosMongo.Application.Dto.Produto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Interfaces;

public interface IProdutosUseCase
{
    Task CriarAsync(CriarProdutoDto dto);
}
