using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Dto.Produto;

public sealed record CriarProdutoDto
(string Descricao, string IdCategoria, decimal Valor);
