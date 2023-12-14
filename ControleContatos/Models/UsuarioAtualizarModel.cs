using ControleContatos.Enums;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class UsuarioAtualizarModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O E-mail informado não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil {  get; set; }
    }
}
