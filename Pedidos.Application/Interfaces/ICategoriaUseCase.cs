using PedidosMongo.Application.Dto.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.Interfaces;

public interface ICategoriaUseCase
{
    Task CriarAsync(CriarCategoriaDto dto);
    Task<List<ObterCategoriaDto>> ObterTodasAsync();
}
