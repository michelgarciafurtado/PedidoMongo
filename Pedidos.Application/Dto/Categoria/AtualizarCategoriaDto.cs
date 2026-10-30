using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Dto.Categoria;

public sealed record AtualizarCategoriaDto
(Guid id, string descricao);
