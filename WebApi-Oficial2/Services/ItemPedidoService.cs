using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi_Oficial2.Data;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Interfaces;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Services {
    public class ItemPedidoService : IItemPedidoService {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ItemPedidoService(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ItemPedidoDTO>> GetAll(CancellationToken ct) {
            var pedido = await _context.Itens.ToListAsync(ct);
            if (pedido is null) throw new ApplicationException("Não existe item no pedido.");
            return _mapper.Map<IEnumerable<ItemPedidoDTO>>(pedido);
        }

        public async Task<ItemPedidoDTO> GetById(int id, CancellationToken ct) {
            var pedido = await _context.Itens.FirstOrDefaultAsync(p => p.ItemPedidoId == id, ct);
            if (pedido is null) throw new ApplicationException("Esse item do pedido nao existe.");
            return _mapper.Map<ItemPedidoDTO>(pedido);
        }
    }
}
