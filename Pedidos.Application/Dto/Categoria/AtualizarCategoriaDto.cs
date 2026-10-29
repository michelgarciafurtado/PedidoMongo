using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Application.Dto.Categoria;

public sealed record AtualizarCategoriaDto
(Guid id, string descricao);
