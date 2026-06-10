using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimosLivroCurso.Dto.Home
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Insira o email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insira a senha!")]
        public string Senha { get; set; }
    }
}