using PedidosMongo.Application.Dto.Produto;
using PedidosMongo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.UseCase.Produtos;

public sealed class ObterProdutosUseCase
{
    private readonly IProdutosConsultas _consultas;

    public ObterProdutosUseCase(IProdutosConsultas consultas)
    {
        _consultas = consultas;
    }

    public async Task<List<ObterProdutoDto>> ObterProdutosAsync()
    {
        return await _consultas.ObterTodosAsync();
    }
}
