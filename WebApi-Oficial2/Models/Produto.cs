using System.ComponentModel.DataAnnotations;

namespace WebApi_Oficial2.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 10000, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [StringLength(255, ErrorMessage = "A descrição pode ter no máximo 255 caracteres.")]
        public String Descricao { get; set; }

        public bool Inativo { get; set; } = false;

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(50, ErrorMessage = "A categoria pode ter no máximo 50 caracteres.")]
        public String Categoria { get; set; }

        public List<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

    }
}
