using System.ComponentModel.DataAnnotations;

namespace WebApi_Oficial2.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        [StringLength(255, ErrorMessage = "O nome pode ter no máximo 255 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo Idade é obrigatório.")]
        [Range(1, 120, ErrorMessage = "A idade deve estar entre 1 e 120 anos.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "CPF inválido. Use o formato 000.000.000-00.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", ErrorMessage = "Telefone inválido. Exemplo: (11) 98765-4321.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        [StringLength(255, ErrorMessage = "O endereço pode ter no máximo 255 caracteres.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [StringLength(255, ErrorMessage = "O e-mail pode ter no máximo 255 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage = "A senha deve ter no mínimo 8 caracteres, com letra maiúscula, minúscula, número e caractere especial.")]
        public string Senha { get; set; }

        public bool Inativo { get; set; } = false;
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
