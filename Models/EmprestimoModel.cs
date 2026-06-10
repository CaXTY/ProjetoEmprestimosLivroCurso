using System.Text.Json.Serialization;

namespace ProjetoEmprestimosLivroCurso.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }

        public int LivroId { get; set; }
        [JsonIgnore]
        public LivroModel Livro { get; set; }
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public DateTime? DataDevolucao { get; set; }

    }
}