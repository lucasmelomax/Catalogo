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
    public class AuthenticateService : IAuthenticate
    {
        AppDbContext _context;
        IConfiguration _configuration;
        public AuthenticateService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(string email, string senha, CancellationToken ct)
        {
            try
            {
                var usuario = await ObterCredenciaisAsync(email, senha, ct);

                var token = GerarToken(usuario.UsuarioId, usuario.NomeCompleto);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return "Erro de autenticação: " + ex.Message;
            }
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

        public async Task<Usuario> ObterCredenciaisAsync(string email, string senha, CancellationToken ct)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha, ct);

            if (usuario is null)
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            return usuario;
        }
    }
}
