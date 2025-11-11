using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.DTO {
    public class PedidoDTO {

        [Key]
        public int PedidoId { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public int UsuarioId { get; set; }

    }
}
