using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Dto.Pedido;

public sealed record CriarEnderecoDto(string Rua, int n, string Bairro, string Cidade, string Cep, string? compl);
