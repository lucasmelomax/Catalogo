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
        public string Senha { get; set; }
    }
}
