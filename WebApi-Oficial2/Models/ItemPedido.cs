using System.ComponentModel.DataAnnotations;
using WebApi_Oficial2.Models;

public class ItemPedido
{
    [Key]
    public int ItemPedidoId { get; set; }

    [Required]
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }

    [Required]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }

    [Required]
    public int Quantidade { get; set; }
}
