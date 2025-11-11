using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Oficial2.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public int UsuarioId { get; set; }

        public bool Inativo { get; set; } = false;

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public List<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
