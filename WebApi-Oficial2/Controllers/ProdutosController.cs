using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Interfaces;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase {

        private readonly string _messageErrorGeneric = "Erro interno no servidor. Tente novamente mais tarde.";
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService) {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Produto>>> GetAll(int id, CancellationToken ct) {
            try {
                var todosProdutos = await _produtoService.GetAll(ct);
                return Ok(todosProdutos);
            }
            catch (KeyNotFoundException ex) {
                return StatusCode(404, new { mensagem = ex.Message });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpGet("Obter/{id}")]
        [Authorize]

        public async Task<IActionResult> Get(int id, CancellationToken ct) {
            try {
                var todosProdutos = await _produtoService.FiltrarPorIdAsync(id, ct);
                return Ok(todosProdutos);
            }
            catch (KeyNotFoundException ex) {
                return StatusCode(404, new { mensagem = ex.Message });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Produto>> Post(ProdutoDTO produtoDTO, CancellationToken ct) {
            try {
                var criado = await _produtoService.CadastrarAsync(produtoDTO, ct);
                return Ok(criado);
            }
            catch (ApplicationException ex) {
                return StatusCode(400, new { mensagem = ex.Message });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpPut("Atualizar/{id}")]
        [Authorize]

        public async Task<ActionResult<Produto>> Put(int id, ProdutoDTO produtoDTO, CancellationToken ct) {
            try {
                var alterado = await _produtoService.AtualizarAsync(id, produtoDTO, ct);
                return Ok(alterado);
            }
            catch (KeyNotFoundException ex) {
                return StatusCode(400, new { mensagem = ex.Message });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpDelete("Deletar/{id}")]

        public async Task<ActionResult> Delete(int id, CancellationToken ct) {
            try {
                await _produtoService.DeletarAsync(id, ct);
                return Ok($"Deletado id: {id}.");
            }
            catch (KeyNotFoundException ex) {
                return StatusCode(400, new { mensagem = ex.Message });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }
    }
}
