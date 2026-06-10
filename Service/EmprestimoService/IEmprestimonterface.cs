using ProjetoEmprestimosLivroCurso.Models;

namespace ProjetoEmprestimosLivroCurso.Services.EmprestimoService
{
    public interface IEmprestimoInterface
    {
        Task<RespostaModel<EmprestimoModel>> Emprestar(int livroId);
    }
}