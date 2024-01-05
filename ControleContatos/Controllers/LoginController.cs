using ControleContatos.Helper;
using ControleContatos.Interface;
using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;

        }
        public IActionResult Index()
        {

            if(_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if(ModelState.IsValid)
                {

                  UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if(usuario != null)
                    {
                        if(usuario.senhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do usuário inválida. Por favor, tente novamente.";
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }

                return View("Index");
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = $"Ops, Ocorreu um erro realizar o login, tente novamente.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Login, redefinirSenhaModel.Email);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.SenhaRedefinida();

                        string assunto = "Sistema de contatos - Nova Senha.";
                        string MensagemSenhaNova = $"Solicitação de altarar senha com sucesso. Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.Email, assunto, MensagemSenhaNova);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu email cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o email. Tente novamente.";
                        }

                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Erro ao tentar redefinir sua senha. Por favor verifique os dados informados.";
                }

                return View("Index");
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = $"Ops, Ocorreu um erro ao tentar redefinir sua senha, tente novamente.";
                return RedirectToAction("Index");
            }
        }
    }
}
