using ProjetoEmprestimosLivroCurso.Dto.Home;
using ProjetoEmprestimosLivroCurso.Models;

namespace ProjetoEmprestimosLivroCurso.Services.HomeService
{
    public interface IHomeInterface
    {
        Task<RespostaModel<UsuarioModel>> RealizarLogin(LoginDto loginDto);
    }
}