using ProjetoEmprestimosLivroCurso.Data;
using ProjetoEmprestimosLivroCurso.Models;
using ProjetoEmprestimosLivroCurso.Services.EmprestimoService;
using ProjetoEmprestimosLivroCurso.Services.LivroService;
using ProjetoEmprestimosLivroCurso.Services.SessaoService;

namespace ProjetoEmprestimosLivroCurso.Services.EmprestimoService
{
    public class EmprestimoService : IEmprestimoInterface
    {
        private readonly ILivroInterface _livroInterface;
        private readonly AppDbContext _context;
        private readonly ISessaoInterface _sessaoInterface;

        public EmprestimoService(ILivroInterface livroInterface, AppDbContext context, ISessaoInterface sessaoInterface)
        {
            _livroInterface = livroInterface;
            _context = context;
            _sessaoInterface = sessaoInterface;

        }

        public async Task<RespostaModel<EmprestimoModel>> Emprestar(int livroId)
        {
            RespostaModel<EmprestimoModel> resposta = new RespostaModel<EmprestimoModel>();
            try
            {
                var sessaoUsuario = _sessaoInterface.BuscarSessao();

                if (sessaoUsuario == null)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "É necessário estar logado para emprestar um livro!";
                    return resposta;
                }

                var livro = await _livroInterface.BuscarLivroPorId(livroId);


                var emprestimo = new EmprestimoModel
                {
                    UsuarioId = sessaoUsuario.Id,
                    LivroId = livro.Id,
                    // Usuario = sessaoUsuario,
                    Livro = livro
                };

                _context.Add(emprestimo);
                await _context.SaveChangesAsync();

                var livroEstoque = await BaixarEstoque(livro);

                resposta.Dados = emprestimo;

                return resposta;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task<LivroModel> BaixarEstoque(LivroModel livro)
        {
            livro.QuantidadeEmEstoque--;
            _context.Update(livro);
            await _context.SaveChangesAsync();

            return livro;
        }

        public async Task<LivroModel> RetornarEstoque(LivroModel livro)
        {
            livro.QuantidadeEmEstoque++;
            _context.Update(livro);
            await _context.SaveChangesAsync();

            return livro;
        }



    }
}