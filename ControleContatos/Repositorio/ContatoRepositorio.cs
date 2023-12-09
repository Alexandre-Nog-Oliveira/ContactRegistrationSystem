using ControleContatos.Data;
using ControleContatos.Interface;
using ControleContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _dbContext;

        public ContatoRepositorio(BancoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ContatoModel BuscarPorId(int id)
        {
            return _dbContext.Contatos.FirstOrDefault(c => c.Id == id);
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _dbContext.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _dbContext.Contatos.Add(contato);
            _dbContext.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = BuscarPorId(contato.Id);

            if (contatoDB == null)
                throw new Exception("Houver um erro ao editar contato");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _dbContext.Contatos.Update(contatoDB);
            _dbContext.SaveChanges();
            return contatoDB;
        }

        public bool Deletar(int id)
        {
            ContatoModel contatoDB = BuscarPorId(id);

            if (contatoDB == null)
                throw new Exception("Houver um erro ao deletar o contato");

            _dbContext.Contatos.Remove(contatoDB);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
