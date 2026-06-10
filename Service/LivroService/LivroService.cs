using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimosLivroCurso.Data;
using ProjetoEmprestimosLivroCurso.Dto.Livro;
using ProjetoEmprestimosLivroCurso.Models;

namespace ProjetoEmprestimosLivroCurso.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private string _caminhoServidor;
        public LivroService(AppDbContext context, IWebHostEnvironment sistema, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _caminhoServidor = sistema.WebRootPath;
        }

        public async Task<LivroModel> BuscarLivroPorId(int? id)
        {
            try
            {

                var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);

                return livro;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LivroModel>> BuscarLivros()
        {
            try
            {

                var livros = await _context.Livros.ToListAsync();
                return livros;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {

                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                //var livro = new LivrosModel
                //{
                //    Titulo = livroCriacaoDto.Titulo,
                //    Descricao = livroCriacaoDto.Descricao,
                //    Autor = livroCriacaoDto.Autor,
                //    Genero = livroCriacaoDto.Genero,
                //    QuantidadeEmEstoque = livroCriacaoDto.QuantidadeEmEstoque,
                //    AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                //    Capa = nomeCaminhoImagem,
                //    ISBN = livroCriacaoDto.ISBN

                //};

                var livro = _mapper.Map<LivroModel>(livroCriacaoDto);
                livro.Capa = nomeCaminhoImagem;

                _context.Add(livro);
                await _context.SaveChangesAsync();

                return livro;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto)
        {
            try
            {
                var livro = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == livroEdicaoDto.Id);

                var nomeCaminhoImagem = "";
                if (foto != null)
                {
                    string caminhaCapaExistente = _caminhoServidor + "\\Imagem\\" + livro.Capa;

                    if (File.Exists(caminhaCapaExistente))
                    {
                        File.Delete(caminhaCapaExistente);
                    }

                    nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                }

                var livroModel = _mapper.Map<LivroModel>(livroEdicaoDto);

                if (nomeCaminhoImagem != "")
                {
                    livroModel.Capa = nomeCaminhoImagem;
                }
                else
                {
                    livroModel.Capa = livro.Capa;
                }

                livroModel.DataDeAlteracao = DateTime.Now;


                _context.Update(livroModel);
                await _context.SaveChangesAsync();

                return livroModel;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto)
        {
            try
            {
                var livroBanco = _context.Livros.FirstOrDefault(livro => livro.ISBN == livroCriacaoDto.ISBN);
                if (livroBanco != null)
                {
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            string caminhoParaSalvarImagens = _caminhoServidor + "\\imagem\\";

            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
            {
                foto.CopyToAsync(stream).Wait();
            }
            return nomeCaminhoImagem;
        }

        public async Task<List<LivroModel>> BuscarLivrosFiltro(string pesquisar)
        {
            try
            {

                var livros = await _context.Livros.Where(livro => livro.Titulo.Contains(pesquisar) || livro.Autor.Contains(pesquisar)).ToListAsync();
                return livros;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmprestimoModel> BuscarLivroPorId(int? id, UsuarioModel usuarioSessao)
        {
            try
            {
                //Usuario deslogado
                if (usuarioSessao == null)
                {
                    var emprestimoSemUsuario = await _context.Emprestimos.Include(livro => livro.Livro).FirstOrDefaultAsync(emprestimo => emprestimo.LivroId == id);

                    if (emprestimoSemUsuario == null)
                    {
                        var livro = await BuscarLivroPorId(id);

                        var emprestimoBanco = new EmprestimoModel
                        {
                            Livro = livro,
                            Usuario = null
                        };

                        return emprestimoBanco;
                    }
                    return emprestimoSemUsuario;
                }

                var emprestimo = await _context.Emprestimos
                        .Include(livro => livro.Livro)
                        .Include(usuario => usuario.Usuario)
                        .FirstOrDefaultAsync(emprestimo => emprestimo.LivroId == id && emprestimo.DataDevolucao == null && emprestimo.Usuario.Id == usuarioSessao.Id);


                if (emprestimo == null)
                {
                    var livro = await BuscarLivroPorId(id);

                    var emprestimoBanco = new EmprestimoModel
                    {
                        Livro = livro,
                        Usuario = usuarioSessao
                    };

                    return emprestimoBanco;
                }

                return emprestimo;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}