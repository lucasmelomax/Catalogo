using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi_Oficial2.Data;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Interfaces;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObterCredenciaisAsync(string email, string senha, CancellationToken ct)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha, ct);

            if (usuario is null)
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            return usuario;
        }

        public async Task<Usuario> CadastrarAsync(UserRegisterRequestDTO usuarioDTO, CancellationToken ct)
        {
            var usuariosExistentes = await FiltrarUsuariosAsync(u => u.Email == usuarioDTO.Email || u.Cpf == usuarioDTO.Cpf);

            if (usuariosExistentes.Any())
                throw new ApplicationException("Usuário com o mesmo email ou CPF já existe.");

            var usuario = new Usuario
            {
                NomeCompleto = usuarioDTO.NomeCompleto,
                Idade = usuarioDTO.Idade,
                Cpf = usuarioDTO.Cpf,
                Telefone = usuarioDTO.Telefone,
                Endereco = usuarioDTO.Endereco,
                Email = usuarioDTO.Email,
                Senha = usuarioDTO.Senha
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(ct);

            return usuario;
        }

        public async Task<List<Usuario>> FiltrarUsuariosAsync(Func<Usuario, bool> Func)
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            if (usuarios is not null && usuarios.Any())
            {
                return usuarios.Where(Func).ToList();
            }
            return new List<Usuario>();
        }

        public async Task<Usuario> FiltrarPorIdAsync(int id, CancellationToken ct)
        {
            var usuario = await _context.Usuarios.FindAsync(new object[] { id }, ct);

            if ((usuario is null))
                throw new KeyNotFoundException("Usuário não encontrado.");
            return usuario;
        }

        public async Task<Usuario> AtualizarAsync(int id, UserRegisterRequestDTO usuarioDTO, CancellationToken ct)
        {
            var usuario = await FiltrarPorIdAsync(id, ct);

            if (usuario is null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            usuario.NomeCompleto = usuarioDTO.NomeCompleto;
            usuario.Idade = usuarioDTO.Idade;
            usuario.Cpf = usuarioDTO.Cpf;
            usuario.Telefone = usuarioDTO.Telefone;
            usuario.Endereco = usuarioDTO.Endereco;
            usuario.Email = usuarioDTO.Email;
            usuario.Senha = usuarioDTO.Senha;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync(ct);

            return usuario;
        }

        public async Task<Usuario> AtualizarParcialAsync(int id, JsonPatchDocument<UserRegisterRequestDTO> patchDoc, CancellationToken ct)
        {
            var usuario = await FiltrarPorIdAsync(id, ct);

            if (usuario is null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            var usuarioDTO = new UserRegisterRequestDTO
            {
                NomeCompleto = usuario.NomeCompleto,
                Idade = usuario.Idade,
                Cpf = usuario.Cpf,
                Telefone = usuario.Telefone,
                Endereco = usuario.Endereco,
                Email = usuario.Email,
                Senha = usuario.Senha
            };

            patchDoc.ApplyTo(usuarioDTO);

            usuario.NomeCompleto = usuarioDTO.NomeCompleto;
            usuario.Idade = usuarioDTO.Idade;
            usuario.Cpf = usuarioDTO.Cpf;
            usuario.Telefone = usuarioDTO.Telefone;
            usuario.Endereco = usuarioDTO.Endereco;
            usuario.Email = usuarioDTO.Email;
            usuario.Senha = usuarioDTO.Senha;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync(ct);

            return usuario;
        }

        public async Task DeletarAsync(int id, CancellationToken ct)
        {
            var usuario = await FiltrarPorIdAsync(id, ct);
            if (usuario is null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync(ct);
        }
    }
}
