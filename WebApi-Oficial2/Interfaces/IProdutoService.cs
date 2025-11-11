using Microsoft.AspNetCore.JsonPatch;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Interfaces {
    public interface IProdutoService : IBaseInterface<Produto, ProdutoDTO> {

        Task<IEnumerable<Produto>> GetAll(CancellationToken ct);

    }
}
