using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimosLivroCurso.Models;

namespace ProjetoEmprestimosLivroCurso.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<EmprestimoModel> Emprestimos { get; set; }

    }
}