using ControleContatos.Interface;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControleContatos.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                bool deletar = _usuarioRepositorio.Deletar(id);
                if (deletar)
                {
                    TempData["MensagemSucesso"] = $"Usuário deletado com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, Ocorreu um erro ao delete o contato, tente novamente.";
                }

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["MensagemErro"] = $"Ops, Ocorreu um erro ao delete o contato, tente novamente.";
                return RedirectToAction("Index");
            }
        }

        // Post
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = $"Usuário {usuario.Login} cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, Ocorreu um erro ao cadastrar usuário, tente novamente. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EditarUsuario(UsuarioAtualizarModel usuarioAtualizar)
        {
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioAtualizar.Id,
                        Nome = usuarioAtualizar.Nome,
                        Email = usuarioAtualizar.Email,
                        Login = usuarioAtualizar.Login,
                        Perfil = usuarioAtualizar.Perfil,
                    };

                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = $"Usuário {usuario.Nome} editado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, Ocorreu um erro ao editar o contato, tente novamente. Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
