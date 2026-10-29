using MongoDB.Driver;
using Pedidos.Application.Dto.Categoria;
using PedidosMongo.Domain.Entidades;
using PedidosMongo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Infra.Repositories;

public class CategoriaRepository:ICategoriaRepository
{
    private readonly IMongoCollection<Categoria> _categoriasCollection;

    public CategoriaRepository(IMongoDatabase database)
    {
        _categoriasCollection = database.GetCollection<Categoria>("Categorias");
    }

    public async Task CriarAsync(Categoria categoria)
    {
        await _categoriasCollection.InsertOneAsync(categoria);
    }

    public async Task<List<Categoria>> ObterTodasAsync()
    {
        return await _categoriasCollection.Find(_ => true).ToListAsync();
    }


}
