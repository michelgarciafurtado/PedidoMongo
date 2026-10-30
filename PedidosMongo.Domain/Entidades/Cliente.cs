using PedidosMongo.Domain.Agregados;
using PedidosMongo.Domain.ObjetosValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.Entidades;

public class Cliente : EntidadeBase
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Tel { get; set; }
    public Endereco Endereco { get; set; }

    public Cliente(string nome, string cpf, string tel, Endereco endereco)
    {
        Nome = nome;
        Cpf = cpf;
        Tel = tel;
        Endereco = endereco;
    }

    public Cliente(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }

}
