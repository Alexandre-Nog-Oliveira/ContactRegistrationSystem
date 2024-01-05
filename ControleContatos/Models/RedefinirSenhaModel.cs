using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "O campo Login é obrigatório para redefinir senha")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o E-mail para redefinir a senha")]
        public string Email { get; set; }
    }
}
