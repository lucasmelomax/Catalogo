using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Interfaces;

namespace WebApi_Oficial2.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPedidoController : ControllerBase {

        private readonly IItemPedidoService _pedidoService;
        private readonly string _messageErrorGeneric = "Erro interno no servidor. Tente novamente mais tarde.";

        public ItemPedidoController(IItemPedidoService pedidoService) {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<ItemPedidoDTO>>> GetAll(CancellationToken ct) {
            try {
                var pedidos = await _pedidoService.GetAll(ct);
                return Ok(pedidos);
            }
            catch (ApplicationException ex) {
                return StatusCode(404, new { mensagem = ex.Message });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ItemPedidoDTO>> GetById(int id, CancellationToken ct) {

            try {
                var pedido = await _pedidoService.GetById(id, ct);
                return Ok(pedido);
            }
            catch (ApplicationException ex) {
                return StatusCode(404, new { mensagem = ex.Message });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }

        }

        }
    }

