using MongoDB.Bson.Serialization.Attributes;
using PedidosMongo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Consultas;

[BsonIgnoreExtraElements]
public sealed class ProdutoLookUp
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public Guid CategoriaId { get; set; }
    public decimal Valor { get; set; }

    public List<Categoria> Categorias { get; set; } = [];
}
