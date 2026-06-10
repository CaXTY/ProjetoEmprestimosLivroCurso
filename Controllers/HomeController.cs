using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimosLivroCurso.Dto.Home;
using ProjetoEmprestimosLivroCurso.Services.HomeService;
using ProjetoEmprestimosLivroCurso.Services.LivroService;
using ProjetoEmprestimosLivroCurso.Services.SessaoService;
using System.Diagnostics;

namespace ProjetoEmprestimosLivroCurso.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IHomeInterface _homeInterface;
        private readonly ILivroInterface _livroInterface;

        public HomeController(ISessaoInterface sessaoInterface, IHomeInterface homeInterface, ILivroInterface livroInterface)
        {
            _sessaoInterface = sessaoInterface;
            _homeInterface = homeInterface;
            _livroInterface = livroInterface;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string pesquisar = null)
        {

            var usuarioSessao = _sessaoInterface.BuscarSessao();
            if (usuarioSessao != null)
            {
                ViewBag.LayoutPagina = "_Layout";
            }
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";
            }

            if (pesquisar == null)
            {
                var livrosBanco = await _livroInterface.BuscarLivros();
                return View(livrosBanco);
            }
            else
            {
                var livrosBanco = await _livroInterface.BuscarLivrosFiltro(pesquisar);
                return View(livrosBanco);
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (_sessaoInterface.BuscarSessao() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Sair()
        {
            _sessaoInterface.RemoverSessao();
            TempData["MensagemSucesso"] = "Usuário deslogado!";
            return RedirectToAction("Login", "Home");
        }


        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            var usuarioSessao = _sessaoInterface.BuscarSessao();

            if (usuarioSessao != null)
            {
                ViewBag.UsuarioLogado = usuarioSessao.Id;
                ViewBag.LayoutPagina = "_Layout";
            }
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";
            }


            var livro = await _livroInterface.BuscarLivroPorId(id, usuarioSessao);


            if (livro.Usuario != null)
            {
                if (livro.Usuario.Emprestimos == null)
                {
                    ViewBag.Emprestimos = "SemEmprestimos";
                }

            }

            return View(livro);

        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var login = await _homeInterface.RealizarLogin(loginDto);

                if (login.Status == false)
                {
                    TempData["MensagemErro"] = login.Mensagem;
                    return View(login.Dados);
                }

                if (login.Dados.Situacao == false)
                {
                    TempData["MensagemErro"] = "Procure o suporte para verificar o status da sua conta!";
                    return View("Login");
                }


                _sessaoInterface.CriarSessao(login.Dados);
                TempData["MensagemSucesso"] = login.Mensagem;

                return RedirectToAction("Index");

            }
            else
            {
                return View(loginDto);
            }
        }


    }
}