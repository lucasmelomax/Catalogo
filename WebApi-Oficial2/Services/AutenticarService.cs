using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi_Oficial2.Data;
using WebApi_Oficial2.Interfaces;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Services
{
    public class AutenticarService : IAutenticarService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;
        public AutenticarService(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        public async Task<string> AutenticarAsync(string email, string senha, CancellationToken ct)
        {
            var usuario = await _usuarioService.ObterCredenciaisAsync(email, senha, ct);

            var token = GerarToken(usuario.UsuarioId, usuario.NomeCompleto);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken GerarToken(int id, string nome)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, nome)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return token;
        }

    }
}
