using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimosLivroCurso.Data;
using ProjetoEmprestimosLivroCurso.Dto.Usuario;
using ProjetoEmprestimosLivroCurso.Models;
using ProjetoEmprestimosLivroCurso.Services.Autenticacao;

namespace ProjetoEmprestimosLivroCurso.Services.UsuarioService
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;
        public UsuarioService(AppDbContext context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int? id)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == id);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            try
            {
                var registros = new List<UsuarioModel>();

                if (id != null)
                {
                    registros = await _context.Usuarios.Where(cliente => cliente.Perfil == 0).Include(e => e.Endereco).ToListAsync();
                }
                else
                {
                    registros = await _context.Usuarios.Where(funcionario => funcionario.Perfil != 0).Include(e => e.Endereco).ToListAsync();
                }

                return registros;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                _autenticacaoInterface.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    NomeCompleto = usuarioCriacaoDto.NomeCompleto,
                    Usuario = usuarioCriacaoDto.Usuario,
                    Email = usuarioCriacaoDto.Email,
                    Perfil = usuarioCriacaoDto.Perfil,
                    Turno = usuarioCriacaoDto.Turno,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                var endereco = new EnderecoModel
                {
                    Logradouro = usuarioCriacaoDto.Logradouro,
                    Numero = usuarioCriacaoDto.Numero,
                    Bairro = usuarioCriacaoDto.Bairro,
                    Estado = usuarioCriacaoDto.Estado,
                    Complemento = usuarioCriacaoDto.Complemento,
                    CEP = usuarioCriacaoDto.CEP,
                    Usuario = usuario
                };


                usuario.Endereco = endereco;


                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return usuarioCriacaoDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> Editar(UsuarioEditarDto usuarioEditarDto)
        {
            try
            {

                var usuarioEditarBanco = await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == usuarioEditarDto.Id);

                if (usuarioEditarBanco != null)
                {
                    usuarioEditarBanco.Turno = usuarioEditarDto.Turno;
                    usuarioEditarBanco.Perfil = usuarioEditarDto.Perfil;
                    usuarioEditarBanco.NomeCompleto = usuarioEditarDto.NomeCompleto;
                    usuarioEditarBanco.Usuario = usuarioEditarDto.Usuario;
                    usuarioEditarBanco.Email = usuarioEditarDto.Email;
                    usuarioEditarBanco.DataAlteracao = DateTime.Now;
                    usuarioEditarBanco.Endereco = _mapper.Map<EnderecoModel>(usuarioEditarDto.Endereco);


                    _context.Update(usuarioEditarBanco);
                    await _context.SaveChangesAsync();

                    return usuarioEditarBanco;
                }

                return usuarioEditarBanco;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> MudarSituacaoUsuario(int id)
        {
            try
            {
                var usuarioMudarSituacao = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == id);

                if (usuarioMudarSituacao != null)
                {
                    if (usuarioMudarSituacao.Situacao == true)
                    {
                        usuarioMudarSituacao.Situacao = false;
                        usuarioMudarSituacao.DataAlteracao = DateTime.Now;
                    }
                    else
                    {
                        usuarioMudarSituacao.Situacao = true;
                        usuarioMudarSituacao.DataAlteracao = DateTime.Now;
                    }

                    _context.Update(usuarioMudarSituacao);
                    await _context.SaveChangesAsync();

                    return usuarioMudarSituacao;
                }

                return usuarioMudarSituacao;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                var mesmoUsuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Email == usuarioCriacaoDto.Email ||
                                                                                usuarioBanco.Usuario == usuarioCriacaoDto.Usuario);

                if (mesmoUsuario == null)
                {
                    return true;
                }

                return false;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}