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
    }
}
