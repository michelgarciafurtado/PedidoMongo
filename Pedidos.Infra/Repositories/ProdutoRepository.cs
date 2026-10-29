using MongoDB.Driver;
using PedidosMongo.Domain.Entidades;
using PedidosMongo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Infra.Repositories;

public class ProdutoRepository : IProdutosRepository
{
    private readonly IMongoCollection<Produto> _produtosCollection;
    private readonly IMongoCollection<Categoria> _categoriasCollection;

    public ProdutoRepository(IMongoDatabase database)
    {
        _produtosCollection = database.GetCollection<Produto>("Produtos");
    }

   
    public async Task CriarAsync(Produto novoProduto)
    {
       await _produtosCollection.InsertOneAsync(novoProduto);
    }
}
