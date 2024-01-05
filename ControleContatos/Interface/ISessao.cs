using ControleContatos.Models;

namespace ControleContatos.Interface
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();
        UsuarioModel BuscarSessaoUsuario();
    }
}
