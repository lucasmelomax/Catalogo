using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Interfaces
{
    public interface IUsuarioService : IBaseInterface<Usuario, UserRegisterRequestDTO>
    {
        Task<Usuario> ObterCredenciaisAsync(string email, string senha, CancellationToken ct);
        Task<List<Usuario>> FiltrarUsuariosAsync(Func<Usuario,bool> func);
        Task<Usuario> AtualizarParcialAsync(int id, JsonPatchDocument<UserRegisterRequestDTO> patchDoc, CancellationToken ct);
    }
}
