using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O E-mail informado não é válido")]
        public string Email {  get; set; }

        [Required(ErrorMessage = "O campo Celular é obrigatório")]
        public string Celular { get; set; }
    }
}
