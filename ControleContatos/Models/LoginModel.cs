using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo Login é obrigatório e não pode ser vázio")]
        public string Login{ get; set; }
        [Required(ErrorMessage = "Digite a senha para fazer o login")]
        public string Senha { get; set; }
    }
}
