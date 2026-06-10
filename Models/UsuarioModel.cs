using ProjetoEmprestimosLivroCurso.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimosLivroCurso.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required]
        public string Usuario { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public bool Situacao { get; set; } = true;
        [Required]
        public PerfilEnum Perfil { get; set; }
        [Required]
        public TurnoEnum Turno { get; set; }
        [Required]
        public byte[] SenhaHash { get; set; }
        [Required]
        public byte[] SenhaSalt { get; set; }

        [Required]
        public EnderecoModel Endereco { get; set; }
        public List<EmprestimoModel> Emprestimos { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}