using Microsoft.EntityFrameworkCore;
using WebApi_Oficial2.Data;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Interfaces;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Services {
    public class ProdutoService : IProdutoService {

        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll(CancellationToken ct) {
            return await _context.Produtos.ToListAsync(ct);
        }
        public async Task<Produto> FiltrarPorIdAsync(int id, CancellationToken ct) {

            var produtos = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id, ct);

            if ((produtos is null))
                throw new KeyNotFoundException("Produto não encontrado.");

            return produtos;

        }

        public async Task<Produto> FiltrarPorNome(string nome, CancellationToken ct) => await _context.Produtos.FirstOrDefaultAsync(p => p.Nome == nome, ct);
            
        
        public async Task<Produto> CadastrarAsync(ProdutoDTO produtoDTO, CancellationToken ct) {

            var produtoExistente = await FiltrarPorNome(produtoDTO.Nome, ct);

            if (produtoExistente is not null) throw new ApplicationException("Produto ja cadastrado no sistema.");

            var produto = new Produto {
                Nome = produtoDTO.Nome,
                Preco = produtoDTO.Preco,
                Descricao = produtoDTO.Descricao,
                Categoria = produtoDTO.Categoria,
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync(ct);


            // receber lista de produto
            //salvar um pedido no banco
            //pegar id do pedido salvo
            //fazer um foreach pra cada produto criando um item pedido com a quantidade do produto e o id do pedido salvo


            return produto;
        }
        public async  Task<Produto> AtualizarAsync(int id, ProdutoDTO produtoDTO, CancellationToken ct) {
            var produto = await FiltrarPorIdAsync(id, ct);

            if (produto is null)
                throw new KeyNotFoundException("Produto não encontrado.");


            produto.Nome = produtoDTO.Nome;
            produto.Preco = produtoDTO.Preco;
            produto.Descricao = produtoDTO.Descricao;
            produto.Categoria = produtoDTO.Categoria;
           

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync(ct);
            return produto;

        }

        public async Task DeletarAsync(int id, CancellationToken ct) {
            var produto = await FiltrarPorIdAsync(id, ct);
            if (produto is null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync(ct);
        }

    }
}
