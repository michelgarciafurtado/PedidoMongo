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

    public Item(Guid produtoId, string descricao, decimal valorUn, int qtd)
    {
        ProdutoId = produtoId;
        Descricao = descricao;
        ValorUn = valorUn;
        Qtd = qtd;
        VlUn = CalculaTotalItem();
    }

    private decimal CalculaTotalItem()
    {
        if (Qtd <= 0)
            new ArgumentException("A quantidade não pode ser nula ou 0");
        if (ValorUn <= 0)
            new ArgumentException("O valor não pode ser nulo");
        return Qtd * ValorUn;
            
    }
}
