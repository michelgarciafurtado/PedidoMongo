using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidosMongo.Application.Dto.Pedido;
using PedidosMongo.Application.Interfaces;

namespace PedidosMongo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController(IPedidoUseCase pedidoUseCase) : ControllerBase
    {
        private readonly IPedidoUseCase _pedidoUseCase = pedidoUseCase;

        [HttpPost]
        public async Task<IActionResult> CriarPedidoAsync(CriarPedidoDto dto)
        {
            await _pedidoUseCase.CriarPedido(dto);
            return Created();
        }
    }
}
