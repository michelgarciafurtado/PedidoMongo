using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosMongo.Application.Dto.Produto;
using PedidosMongo.Application.Interfaces;
using PedidosMongo.Application.UseCase.Produtos;

namespace PedidosMongo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosUseCase _produtousecase;
        private readonly ObterProdutosUseCase _consultas;

        public ProdutosController(IProdutosUseCase produtousecase, ObterProdutosUseCase consultas)
        {
            _produtousecase = produtousecase;
            _consultas = consultas;
        }

        [HttpPost]
        public async Task<ActionResult> CriarAsync(CriarProdutoDto dto)
        {
            await _produtousecase.CriarAsync(dto);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodosAsync()
        {
            var produtos = await _consultas.ObterProdutosAsync();
            return Ok(produtos);
        }
    }
}
