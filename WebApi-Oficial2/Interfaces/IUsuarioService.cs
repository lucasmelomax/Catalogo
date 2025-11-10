using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> ObterCredenciaisAsync(string email, string senha, CancellationToken ct);
        Task<Usuario> CadastrarAsync(UserRegisterRequestDTO usuarioDTO, CancellationToken ct);
        Task<List<Usuario>> FiltrarUsuariosAsync(Func<Usuario,bool> func);
        Task<Usuario> FiltrarPorIdAsync(int id, CancellationToken ct);
        Task<Usuario> AtualizarAsync(int id, UserRegisterRequestDTO usuarioDTO, CancellationToken ct);
        Task<Usuario> AtualizarParcialAsync(int id, JsonPatchDocument<UserRegisterRequestDTO> patchDoc, CancellationToken ct);
        Task DeletarAsync(int id, CancellationToken ct);
    }
}
