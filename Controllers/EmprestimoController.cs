using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimosLivroCurso.Services.EmprestimoService;
using ProjetoEmprestimosLivroCurso.Services.LivroService;
using ProjetoEmprestimosLivroCurso.Services.SessaoService;

namespace ProjetoEmprestimosLivroCurso.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly ILivroInterface _livroInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;

        public EmprestimoController(ISessaoInterface sessaoInterface, ILivroInterface livroInterface, IEmprestimoInterface emprestimoInterface)
        {
            _sessaoInterface = sessaoInterface;
            _livroInterface = livroInterface;
            _emprestimoInterface = emprestimoInterface;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Emprestar(int id)
        {
            var sessaoUsuario = _sessaoInterface.BuscarSessao();
            if (sessaoUsuario == null)
            {
                TempData["MensagemErro"] = "É necessário estar logado para emprestar livros!";
                return RedirectToAction("Login", "Home");
            }

            var emprestimo = await _emprestimoInterface.Emprestar(id);

            TempData["MensagemSucesso"] = "Emprestimo realizado com sucesso!";

            return RedirectToAction("Index", "Home");
        }

    }
}