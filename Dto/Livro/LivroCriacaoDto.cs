using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimosLivroCurso.Dto.Livro
{
    public class LivroCriacaoDto
    {
        [Required(ErrorMessage = "Insira um título!")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira um descrição!")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o código ISBN!")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o autor!")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira o Genero!")]
        public string Genero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o ano de publicacao!")]
        public int AnoPublicacao { get; set; }
        [Required(ErrorMessage = "Insira uma quantidade em estoque!")]
        public int QuantidadeEmEstoque { get; set; }
    }
}