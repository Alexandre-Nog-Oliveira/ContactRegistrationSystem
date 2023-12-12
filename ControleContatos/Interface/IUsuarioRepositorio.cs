using ControleContatos.Models;
using System.Collections.Generic;

namespace ControleContatos.Interface
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel contato);
        UsuarioModel Atualizar(UsuarioModel contato);
        bool Deletar(int id);
    }
}
