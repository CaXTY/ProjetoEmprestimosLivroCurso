using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimosLivroCurso.Dto.Endereco
{
    public class EnderecoEditarDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Logradouro!")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Digite o Bairro!")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite o Numero!")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Digite o CEP!")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Digite o Estado!")]
        public string Estado { get; set; }
        public string? Complemento { get; set; }
        public int UsuarioId { get; set; }
    }
}