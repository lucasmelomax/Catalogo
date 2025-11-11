using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi_Oficial2.DTO {
    public class ProdutoDTO {

        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 10000, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [StringLength(255, ErrorMessage = "A descrição pode ter no máximo 255 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(50, ErrorMessage = "A categoria pode ter no máximo 50 caracteres.")]
        public string Categoria { get; set; }

        [JsonIgnore]
        public List<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
