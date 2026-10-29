using MongoDB.Driver;
using Pedidos.Application.Dto.Categoria;
using PedidosMongo.Application.Consultas;
using PedidosMongo.Application.Dto.Produto;
using PedidosMongo.Application.Interfaces;
using PedidosMongo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Infra.Consultas;

public sealed class ProdutosConsulta : IProdutosConsultas
{
    private readonly IMongoCollection<Produto> _produtosCollection;
    private readonly IMongoCollection<Categoria> _categoriasCollection;

    public ProdutosConsulta(IMongoDatabase database)
    {
        _produtosCollection = database.GetCollection<Produto>("Produtos");
        _categoriasCollection = database.GetCollection<Categoria>("Categorias");
    }

    public Task<ObterProdutoDto?> ObterPorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ObterProdutoDto>> ObterTodosAsync()
    {
        var resultado = await _produtosCollection
            .Aggregate()
            .Lookup<Produto, Categoria, ProdutoLookUp>(
                _categoriasCollection,
                produto => produto.CategoriaId,
                categoria => categoria.Id,
                resultado => resultado.Categorias
            ).ToListAsync();

        return resultado.Select(produto =>
                new ObterProdutoDto(
                        produto.Id.ToString(),
                        produto.Descricao,
                        new ObterCategoriaDto(
                            produto.Categorias.First().Id.ToString(),
                            produto.Categorias.First().Descricao
                         ),
                        produto.Valor
                        )
                ).ToList();
    }
}
