using PedidosMongo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.Interfaces;

public interface IProdutosRepository
{
    Task CriarAsync(Produto novoProduto);
}
