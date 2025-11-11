using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Interfaces {
    public interface IPedido {
        Task<Pedido?> ObterPorIdAsync(int id, CancellationToken ct);
        Task<ItemPedido> AdicionarItemAsync(int pedidoId, ItemPedidoDTO dto, CancellationToken ct);
        Task<bool> RemoverItemAsync(int pedidoId, int itemId, CancellationToken ct);
    }
}
