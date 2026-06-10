using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimosLivroCurso.Services.UsuarioService;

namespace ProjetoEmprestimosLivroCurso.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public ClienteController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }
        public async Task<ActionResult> Index(int? id)
        {
            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }
    }
}