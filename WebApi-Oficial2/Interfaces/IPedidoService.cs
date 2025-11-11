using WebApi_Oficial2.DTO;

namespace WebApi_Oficial2.Interfaces {
    public interface IPedidoService {

        Task<IEnumerable<PedidoDTO>> GetAll(CancellationToken ct);
        Task<PedidoDTO> GetById(int id, CancellationToken ct);
        Task<PedidoDTO> Create(PedidoDTO dto, CancellationToken ct);
        Task Delete(int id, CancellationToken ct);
    }
}
