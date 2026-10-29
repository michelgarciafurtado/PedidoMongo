using PedidosMongo.Domain.Agregados;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.Entidades;

public sealed class Categoria:EntidadeBase
{
    public string Descricao { get; set; }
    public Categoria(string descricao)
    {
        Descricao = descricao;
    }
}
