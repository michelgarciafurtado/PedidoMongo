using PedidosMongo.Domain.Agregados;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.Entidades;

public class Item:EntidadeBase
{
    public Guid ProdutoId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorUn { get; set; }
    public int Qtd { get; set; }
    public decimal VlUn { get; set; }
}
