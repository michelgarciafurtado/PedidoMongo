using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.ObjetosValor;

public sealed record Endereco(string Rua, int n, string Bairro, string Cidade, string Cep, string? compl);
