using PedidosMongo.Domain.Agregados;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.Entidades;

public sealed class Produto:EntidadeBase
{
    public Produto(string descricao, Guid categoriaId, Categoria? categoria, decimal valor)
    {
        Descricao = descricao;
        CategoriaId = categoriaId;
        Categoria = categoria;
        Valor = valor;
    }

    public Produto(Guid id, string descricao, Categoria categoria, decimal valor)
    {
        Id = id;
        Descricao = descricao;
        CategoriaId = categoria.Id;
        Categoria = categoria;
        Valor = valor;
    }

    public string Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public decimal Valor { get; set; }
}
