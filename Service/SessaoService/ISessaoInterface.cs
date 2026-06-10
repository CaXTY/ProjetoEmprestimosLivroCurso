using ProjetoEmprestimosLivroCurso.Models;

namespace ProjetoEmprestimosLivroCurso.Services.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();
    }
}