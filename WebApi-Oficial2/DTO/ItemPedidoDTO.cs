using System.ComponentModel.DataAnnotations;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.DTO {
    public class ItemPedidoDTO {

        [Key]
        public int ItemPedidoId { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}
