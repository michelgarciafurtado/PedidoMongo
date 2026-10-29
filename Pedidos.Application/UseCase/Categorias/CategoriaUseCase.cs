using Pedidos.Application.Dto.Categoria;
using Pedidos.Application.Interfaces;
using PedidosMongo.Domain.Entidades;
using PedidosMongo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Application.UseCase.Categorias;

public class CategoriaUsecase(ICategoriaRepository repository) : ICategoriaUseCase
{
    private readonly ICategoriaRepository _repository = repository;

    public async Task CriarAsync(CriarCategoriaDto dto)
    {
        Categoria categoria = new Categoria(dto.Descricao);
        await _repository.CriarAsync(categoria);
    }

    public async Task<List<ObterCategoriaDto>> ObterTodasAsync()
    {
        var categorias = await _repository.ObterTodasAsync();
        return categorias.Select(c => new ObterCategoriaDto(c.Id.ToString(), c.Descricao))
            .ToList();
    }
}
