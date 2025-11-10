using System.IdentityModel.Tokens.Jwt;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Interfaces
{
    public interface IAuthenticate
    {
        Task<string> AuthenticateAsync(string email, string senha, CancellationToken ct);
        Task<Usuario> ObterCredenciaisAsync(string email, string senha, CancellationToken ct);
        JwtSecurityToken GerarToken(int id, string nome);
    }
}
