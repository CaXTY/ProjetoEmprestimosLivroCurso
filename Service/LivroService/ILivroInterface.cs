using ProjetoEmprestimosLivroCurso.Dto.Livro;
using ProjetoEmprestimosLivroCurso.Models;

namespace ProjetoEmprestimosLivroCurso.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<List<LivroModel>> BuscarLivros();
        Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
        bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);
        Task<LivroModel> BuscarLivroPorId(int? id);
        Task<EmprestimoModel> BuscarLivroPorId(int? id, UsuarioModel usuarioSessao);
        Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto);
        Task<List<LivroModel>> BuscarLivrosFiltro(string pesquisar);

    }
}