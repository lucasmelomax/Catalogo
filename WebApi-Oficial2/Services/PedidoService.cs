using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi_Oficial2.Data;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Interfaces;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Services {
    public class PedidoService : IPedidoService {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PedidoService(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PedidoDTO> Create(PedidoDTO dto, CancellationToken ct) {
            var pedido = _mapper.Map<Pedido>(dto);
            var userPedido = await _context.Usuarios.FirstOrDefaultAsync(p => p.UsuarioId == pedido.UsuarioId, ct); ;
            if(userPedido is null) throw new ApplicationException("Este usuario nao existe.");
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync(ct);

            return _mapper.Map<PedidoDTO>(pedido);
        }

        public async Task Delete(int id, CancellationToken ct) {
            var deletado = await GetById(id, ct);
            if (deletado is null) throw new ApplicationException("Erro ao deletar pedido.");
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<PedidoDTO>> GetAll(CancellationToken ct) {
            var pedido = await _context.Pedidos.ToListAsync();
            if (pedido is null) throw new ApplicationException("Não existe pedidos.");
            return _mapper.Map<IEnumerable<PedidoDTO>>(pedido);
        }

        public async Task<PedidoDTO> GetById(int id, CancellationToken ct) {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.PedidoId == id, ct);
            if (pedido is null) throw new ApplicationException("Esse pedido nao existe.");
            return _mapper.Map<PedidoDTO>(pedido);
        }
    }
}
