using System.IdentityModel.Tokens.Jwt;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Interfaces
{
    public interface IAutenticarService
    {
        Task<string> AutenticarAsync(string email, string senha, CancellationToken ct);
        JwtSecurityToken GerarToken(int id, string nome);
    }
}
