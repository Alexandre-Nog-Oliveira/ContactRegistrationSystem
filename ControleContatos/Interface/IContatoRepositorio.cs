using ControleContatos.Models;
using System.Collections.Generic;

namespace ControleContatos.Interface
{
    public interface IContatoRepositorio
    {
        ContatoModel BuscarPorId(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Deletar(int id);
    }
}
