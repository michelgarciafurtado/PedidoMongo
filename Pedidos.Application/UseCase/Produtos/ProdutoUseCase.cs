using PedidosMongo.Application.Dto.Produto;
using PedidosMongo.Application.Interfaces;
using PedidosMongo.Domain.Entidades;
using PedidosMongo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.UseCase.Produtos;

public class ProdutoUseCase:IProdutosUseCase
{
    private readonly IProdutosRepository _repository;
    public ProdutoUseCase(IProdutosRepository repository) => _repository = repository;
    public async Task CriarAsync(CriarProdutoDto dto)
    {
        Produto NovoProduto = new Produto(dto.Descricao, Guid.Parse(dto.IdCategoria), null,dto.Valor);
        await _repository.CriarAsync(NovoProduto);
    }
}
