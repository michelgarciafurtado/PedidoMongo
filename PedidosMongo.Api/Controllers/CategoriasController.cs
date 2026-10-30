using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosMongo.Application.Dto.Categoria;
using PedidosMongo.Application.Interfaces;

namespace PedidosMongo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController(ICategoriaUseCase usecase) : ControllerBase
    {
        private readonly ICategoriaUseCase _usecase = usecase;

        [HttpPost]
        public async Task<IActionResult> CriarAsync([FromBody] CriarCategoriaDto dto)
        {
           await _usecase.CriarAsync(dto);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> ObterCategorias()
        {
            var categorias = await _usecase.ObterTodasAsync();
            return Ok(categorias);
        }
    }
}
