using WebApi_Oficial2.DTO;

namespace WebApi_Oficial2.Interfaces {
    public interface IItemPedidoService {

        Task<IEnumerable<ItemPedidoDTO>> GetAll(CancellationToken ct);
        Task<ItemPedidoDTO> GetById(int id, CancellationToken ct);
        Task<ItemPedidoDTO> Create(ItemPedidoDTO dto, CancellationToken ct);
        Task Delete(int id, CancellationToken ct);

    }
}
