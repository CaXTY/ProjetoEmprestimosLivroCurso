using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimosLivroCurso.Services.UsuarioService;

namespace ProjetoEmprestimosLivroCurso.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public FuncionarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }
        public async Task<ActionResult> Index()
        {
            var funcionarios = await _usuarioInterface.BuscarUsuarios(null);
            return View(funcionarios);
        }
    }
}