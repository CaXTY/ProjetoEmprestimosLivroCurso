using ProjetoEmprestimosLivroCurso.Dto.Usuario;
using ProjetoEmprestimosLivroCurso.Models;

namespace ProjetoEmprestimosLivroCurso.Services.UsuarioService
{
    public interface IUsuarioInterface
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);
        Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<UsuarioModel> BuscarUsuarioPorId(int? id);

        Task<UsuarioModel> MudarSituacaoUsuario(int id);
        Task<UsuarioModel> Editar(UsuarioEditarDto usuarioEditarDto);

    }
}