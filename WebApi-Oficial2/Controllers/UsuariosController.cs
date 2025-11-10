using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Interfaces;


namespace WebApi_Oficial2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly string _messageErrorGeneric = "Erro interno no servidor. Tente novamente mais tarde.";
        private readonly IAutenticarService _authenticateService;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IAutenticarService authenticateService, IUsuarioService usuarioService)
        {
            _authenticateService = authenticateService;
            _usuarioService = usuarioService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Logar(UserLoginRequestDTO loginDTO)
        {
            try
            {
                var token = await _authenticateService.AutenticarAsync(loginDTO.Email, loginDTO.Senha, CancellationToken.None);
                return Ok(new { Token = token });

            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(400, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UserRegisterRequestDTO cadastrarUsuarioDTO, CancellationToken ct)
        {
            try
            {
                var usuario = await _usuarioService.CadastrarAsync(cadastrarUsuarioDTO, ct);
                return Ok(usuario);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var usuarios = await _usuarioService.FiltrarUsuariosAsync(u => true);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpGet("Obter/{id}")]
        [Authorize]
        public async Task<IActionResult> ListarPorId(int id, CancellationToken ct)
        {
            try
            {
                var usuario = await _usuarioService.FiltrarPorIdAsync(id, ct);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpPut("Atualizar/{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(int id, UserRegisterRequestDTO usuarioDTO, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuario = await _usuarioService.AtualizarAsync(id, usuarioDTO, ct);

                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpPatch("AtualizarParcial/{id}")]
        [Authorize]
        public async Task<IActionResult> AtualizarParcial(int id, [FromBody] JsonPatchDocument<UserRegisterRequestDTO> patchDoc, CancellationToken ct)
        {
            try
            {
                var usuario = await _usuarioService.AtualizarParcialAsync(id, patchDoc, ct);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }

        [HttpDelete("Deletar/{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(int id, CancellationToken ct)
        {
            try
            {
                await _usuarioService.DeletarAsync(id, ct);
                return Ok(new { mensagem = "Usuário deletado com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = _messageErrorGeneric });
            }
        }
    }
}
