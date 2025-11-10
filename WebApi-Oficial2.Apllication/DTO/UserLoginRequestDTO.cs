using System.ComponentModel.DataAnnotations;

namespace WebApi_Oficial2.DTO
{
    public class UserLoginRequestDTO
    {
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [StringLength(255, ErrorMessage = "O e-mail pode ter no máximo 255 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage = "A senha deve ter no mínimo 8 caracteres, com letra maiúscula, minúscula, número e caractere especial.")]
        public string Senha { get; set; }
    }
}
