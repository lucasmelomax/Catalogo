using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Interfaces {
    public interface IBaseInterface<T, U> {

        Task<T> CadastrarAsync(U dto, CancellationToken ct);
        Task<T> FiltrarPorIdAsync(int id, CancellationToken ct);
        Task<T> AtualizarAsync(int id, U dto, CancellationToken ct);
        Task DeletarAsync(int id, CancellationToken ct);

    }
}
