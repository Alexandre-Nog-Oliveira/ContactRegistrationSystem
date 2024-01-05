using ControleContatos.Data;
using ControleContatos.Interface;
using ControleContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _dbContext;

        public UsuarioRepositorio(BancoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _dbContext.Usuarios.FirstOrDefault(c => c.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel BuscarPorEmailLogin(string login, string email)
        {
            return _dbContext.Usuarios.FirstOrDefault(c => c.Email.ToUpper() == email.ToUpper() && c.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _dbContext.Usuarios.FirstOrDefault(c => c.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _dbContext.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.setSenhaHash();
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);

            if (usuarioDB == null)
                throw new Exception("Houver um erro ao editar usuário");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.Senha = usuario.Senha;
            usuarioDB.DataAtualizacao = DateTime.Now;
            

            _dbContext.Usuarios.Update(usuarioDB);
            _dbContext.SaveChanges();
            return usuarioDB;
        }

        public bool Deletar(int id)
        {
            UsuarioModel usuarioDB = BuscarPorId(id);

            if (usuarioDB == null)
                throw new Exception("Houver um erro ao deletar o usuário");

            _dbContext.Usuarios.Remove(usuarioDB);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
