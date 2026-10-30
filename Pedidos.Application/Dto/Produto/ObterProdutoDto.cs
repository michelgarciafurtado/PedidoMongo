using PedidosMongo.Application.Dto.Categoria;

namespace PedidosMongo.Application.Dto.Produto;

public sealed record ObterProdutoDto(string Id, string Descricao, ObterCategoriaDto CategoriaDto, decimal Valor);
