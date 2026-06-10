using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimosLivroCurso.Models
{
    public class LivroModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }  
        [Required]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public string Capa { get; set; } = string.Empty;
        [Required]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public string Autor { get; set; } = string.Empty;
        [Required]
        public string Genero { get; set; } = string.Empty;
        [Required]
        public int AnoPublicacao { get; set; }
        [Required]
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataDeCadastro { get; set; } = DateTime.Now;
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now;

    }
}
